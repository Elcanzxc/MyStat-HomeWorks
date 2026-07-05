import { use, useEffect, useState } from "react";
import {
  FlatList,
  TextInput,
  useWindowDimensions,
  View,
  StyleSheet,
  Text,
  ActivityIndicator,
  TouchableOpacity,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { RECIPES } from "../data/recipes";
import RecipeCard from "../components/RecipeCard";
import PressableCard from "../components/PressableCard";

const RecipeListScreen = () => {
  const navigation = useNavigation();
  const [quary, setQuary] = useState("");
  const { width, height } = useWindowDimensions();
  const numColumns = width > height ? 3 : 2;
  // const filtered = RECIPES.filter((item) =>
  //   item.name.toLowerCase().includes(quary.toLowerCase()),
  // );
  const [recipes, setRecipes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [debouncedQuery, setDebouncedQuery] = useState(quary);
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedQuery(quary);
    }, 500);

    return () => clearTimeout(timer);
  }, [quary]);
  useEffect(() => {
    GetMeals();
  }, [debouncedQuery]);
  const GetMeals = async () => {
    try {
      setLoading(true);

      setError(null);
      const res = await fetch(
        `https://www.themealdb.com/api/json/v1/1/search.php?s=${debouncedQuery}`,
      );

      const json = await res.json();
      const mapped = (json.meals || []).map((m) => ({
        id: m.idMeal,
        name: m.strMeal,
        category: m.strCategory,
        area: m.strArea,
        thumb: m.strMealThumb,
      }));
      setRecipes(mapped);
    } catch (err) {
      console.log(err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <View style={styles.screen}>
      <TextInput
        value={quary}
        onChangeText={setQuary}
        placeholder="Поиск рецепта..."
        placeholderTextColor={"#94a3b8"}
        style={styles.search}
      />
      {loading ? (
        <View style={styles.loader}>
          <ActivityIndicator size="large" color="#61DAFB" />
        </View>
      ) : error ? (
        <View style={styles.errorBox}>
          <Text style={styles.errorText}>Что-то пошло не так 😕</Text>

          <Text style={styles.errorSubText}>{error}</Text>

          <TouchableOpacity style={styles.retryBtn} onPress={GetMeals}>
            <Text style={styles.retryText}>Повторить</Text>
          </TouchableOpacity>
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
              onPress={() => {
                navigation.navigate("RecipeDetail", { recipe: item });
              }}
              style={{ flex: 1 / numColumns, padding: 6 }}
            >
              <RecipeCard recipe={item} />
            </PressableCard>
          )}
          ListEmptyComponent={
            <Text style={styles.empty}>Ничего не найдено</Text>
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
  empty: {
    textAlign: "center",
    color: "#64748b",
    marginTop: 40,
  },
  loader: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  errorBox: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    paddingHorizontal: 20,
  },

  errorText: {
    fontSize: 18,
    fontWeight: "600",
    marginBottom: 8,
    color: "#0f172a",
  },

  errorSubText: {
    fontSize: 14,
    color: "#64748b",
    marginBottom: 16,
    textAlign: "center",
  },

  retryBtn: {
    backgroundColor: "#61DAFB",
    paddingHorizontal: 18,
    paddingVertical: 10,
    borderRadius: 10,
  },

  retryText: {
    color: "#fff",
    fontWeight: "600",
  },
});

export default RecipeListScreen;
