import {
  ActivityIndicator,
  FlatList,
  StyleSheet,
  Text,
  TextInput,
  useWindowDimensions,
  View,
  Pressable,
  ScrollView,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { RECIPES } from "../data/recipes";
import RecipeCard from "../components/RecipeCard";
import { useEffect, useState } from "react";
import PressableCard from "../components/PressableCard";
import { load, save } from "../utils/storage";
import { useSQLiteContext } from "expo-sqlite";

const RecipeListScreen = () => {
  const db = useSQLiteContext();
  const [recipes, setRecipes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigation = useNavigation();
  const [query, setQuery] = useState("");
  const { width, height } = useWindowDimensions();
  const [isOpen, setIsOpen] = useState(false);
  const [countries, setCountries] = useState([]);
  const [selectedCountry, setSelectedCountry] = useState("All");
  const [selectedLetter, setSelectedLetter] = useState(null);
  const [isSmallCard, setIsSmallCard] = useState(false);
  
  useEffect(() => {
    load("isSmallCard", false).then(setIsSmallCard);
  }, []);

  const toggleCardSize = async () => {
    const nextValue = !isSmallCard;
    setIsSmallCard(nextValue);
    await save("isSmallCard", nextValue);
  };

  const numColumns = isSmallCard ? (width > height ? 4 : 3) : (width > height ? 3 : 2);
  const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split("");

  async function incOpenCount(id) {
    const row = await db.getFirstAsync(
      `SELECT count FROM openCounts WHERE recipeId = ?`,
      id,
    );

    if (row) {
      await db.runAsync(
        `UPDATE openCounts
        SET count = count + 1
        WHERE recipeId = ?`,
        id,
      );
    } else {
      await db.runAsync(
        `INSERT INTO openCounts (recipeId, count)
        VALUES (?, 1)`,
        id,
      );
    }
  }

  useEffect(() => {
    async () => {
      const q = await load("lastQuery");
      if (q) {
        setQuery(q);
      }
    };
  }, []);

  useEffect(() => {
    save("lastQuery", query);
  }, [query]);

  const getMeals = async () => {
    try {
      setLoading(true);
      setError(null);
      let url = `https://www.themealdb.com/api/json/v1/1/search.php?s=${query}`;
      if (selectedLetter) {
        url = `https://www.themealdb.com/api/json/v1/1/search.php?f=${selectedLetter}`;
      } else if (selectedCountry !== "All") {
        url = `https://www.themealdb.com/api/json/v1/1/filter.php?a=${selectedCountry}`;
      }
      const res = await fetch(url);
      const json = await res.json();
      const mapped = (json.meals || []).map((m) => ({
        id: m.idMeal,
        name: m.strMeal,
        category: m.strCategory,
        area: m.strArea,
        thumb: m.strMealThumb,
      }));
      setRecipes(mapped);
    } catch (err) {к
      setError("Нет сети");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    (async () => {
      const res = await fetch(
        "https://www.themealdb.com/api/json/v1/1/list.php?a=list",
      );
      const json = await res.json();
      const mapped = (json.meals || []).map((i) => ({
        area: i.strArea,
        country: i.strCountry,
      }));
      setCountries(mapped);
    })();
  }, []);

  useEffect(() => {
    let timeout = setTimeout(() => {
      getMeals();
    }, 500);
    return () => clearTimeout(timeout);
  }, [query, selectedCountry, selectedLetter]);

  return (
    <View style={styles.screen}>
      <TextInput
        value={query}
        onChangeText={(text) => {
          setQuery(text);
          if (text) {
             setSelectedLetter(null);
             setSelectedCountry("All");
          }
        }}
        placeholder="Searching recipe"
        placeholderTextColor={"#94a3b8"}
        style={styles.search}
      />
      <View style={styles.lettersContainer}>
        <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.letters}>
          {alphabet.map((letter) => (
            <Pressable
              key={letter}
              onPress={() => {
                setSelectedLetter(letter);
                setQuery("");
                setSelectedCountry("All");
              }}
              style={[styles.letterChip, selectedLetter === letter && styles.selectedLetterChip]}
            >
              <Text style={[styles.letterText, selectedLetter === letter && styles.selectedLetterText]}>{letter}</Text>
            </Pressable>
          ))}
        </ScrollView>
      </View>
      <View style={{ flexDirection: "row", justifyContent: "space-between", alignItems: "center", marginBottom: 8, paddingHorizontal: 6 }}>
        <Pressable
          onPress={() => {
            setIsOpen((prev) => !prev);
          }}
        >
          <Text>{isOpen ? "🔍 Close filter" : "🔍 Filter"}</Text>
        </Pressable>
        <Pressable onPress={toggleCardSize}>
          <Text>{isSmallCard ? "🟦 Крупные карточки" : "🔲 Мелкие карточки"}</Text>
        </Pressable>
      </View>
      {isOpen ? (
        <View style={styles.filterContainer}>
          <Pressable
            onPress={() => {
              setSelectedCountry("All");
              setIsOpen(false);
            }}
          >
            <Text>All</Text>
          </Pressable>

          <ScrollView style={{ maxHeight: 200 }}>
            {countries.map((item) => (
              <Pressable
                key={item.area}
                onPress={() => {
                  setSelectedCountry(item.area);
                  setSelectedLetter(null);
                  setQuery("");
                  setIsOpen(false);
                }}
              >
                <Text>{item.area}</Text>
              </Pressable>
            ))}
          </ScrollView>
        </View>
      ) : (
        <></>
      )}
      {loading ? (
        <ActivityIndicator
          style={{ flex: 1, justifyContent: "center", alignItems: "center" }}
          size={"large"}
          color="#61DAFB"
        />
      ) : error ? (
        <View style={{ flex: 1, alignItems: "center" }}>
          <Text
            style={{ textAlign: "center", marginTop: 40, color: "#FF0000" }}
          >
            {error}
          </Text>
          <Pressable
            style={{
              marginTop: 20,
              padding: 20,
              backgroundColor: "#73C2FB",
              borderRadius: 12,
            }}
            onPress={() => {
              getMeals();
            }}
          >
            <Text>Повторить</Text>
          </Pressable>
        </View>
      ) : (
        <FlatList
          data={recipes}
          key={numColumns}
          numColumns={numColumns}
          keyExtractor={(item) => item.id}
          contentContainerStyle={{ padding: 6 }}
          renderItem={({ item }) => (
            <PressableCard
              onPress={async () => {
                const recent = await load("recent", []);
                const next = [item, ...recent.filter((r) => r.id !== item.id)].slice(0, 20);
                await save("recent", next);
                await incOpenCount(item.id)
                navigation.navigate("RecipeDetail", { recipe: item });
              }}
              style={{ flex: 1 / numColumns, padding: 7 }}
            >
              <RecipeCard recipe={item} />
            </PressableCard>
          )}
          ListEmptyComponent={
            <Text style={styles.empty}>Nothing is found</Text>
          }
        />
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  screen: {
    flex: 1,
    paddingTop: 20,
    paddingHorizontal: 8,
    backgroundColor: "#F4F7FA",
  },
  search: {
    backgroundColor: "#fff",
    borderRadius: 10,
    paddingHorizontal: 14,
    paddingVertical: 12,
    fontSize: 16,
    marginHorizontal: 6,
    marginBottom: 8,
    borderWidth: 1,
    borderColor: "#e2e8f0",
  },
  empty: { textAlign: "center", color: "#6478b", marginTop: 40 },
  lettersContainer: { maxHeight: 50, marginBottom: 10 },
  letters: { flexDirection: "row", paddingHorizontal: 6 },
  letterChip: { paddingHorizontal: 12, paddingVertical: 8, marginHorizontal: 4, backgroundColor: "#fff", borderRadius: 16, borderWidth: 1, borderColor: "#e2e8f0" },
  selectedLetterChip: { backgroundColor: "#61DAFB", borderColor: "#61DAFB" },
  letterText: { color: "#1e293b", fontWeight: "bold" },
  selectedLetterText: { color: "#fff" },
});

export default RecipeListScreen;
