import { useEffect, useState } from "react";
import { useSQLiteContext } from "expo-sqlite";
import {
  Image,
  Pressable,
  ScrollView,
  Share,
  StyleSheet,
  Text,
  View,
  Linking,
  TextInput,
  Button,
  FlatList,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { useRoute } from "@react-navigation/native";
import RecipeCard from "../components/RecipeCard";
import PressableCard from "../components/PressableCard";
import { load, save } from "../utils/storage";

const RecipeDetailsScreen = () => {
  const db = useSQLiteContext();
  const [text, setText] = useState("");

  const { recipe } = useRoute().params;
  const navigation = useNavigation();
  useEffect(() => {
    navigation.setOptions({ title: recipe.name });
  }, [recipe]);

  const [full, setFull] = useState();
  const [instructions, setInstructions] = useState("");
  const [smallInstructions, setSmallInstructions] = useState("");
  const [instructionsAreFull, setInstructionsAreFull] = useState(false);
  const [similar, setSimilar] = useState([]);

  useEffect(() => {
    if (recipe?.category) {
      fetch(`https://www.themealdb.com/api/json/v1/1/filter.php?c=${recipe.category}`)
        .then((r) => r.json())
        .then((data) => {
          const meals = data.meals || [];
          const mapped = meals
            .filter((m) => m.idMeal !== recipe.id)
            .slice(0, 5)
            .map((m) => ({
              id: m.idMeal,
              name: m.strMeal,
              category: recipe.category,
              thumb: m.strMealThumb,
            }));
          setSimilar(mapped);
        })
        .catch(console.error);
    }
  }, [recipe.category, recipe.id]);

  const saveNote = async () => {
    await save(`note_${recipe.id}`, text);
  };

  useEffect(() => {
    load(`note_${recipe.id}`, "").then(setText);
  }, [recipe.id]);

  const getRecipe = () => {
    fetch(`https://www.themealdb.com/api/json/v1/1/lookup.php?i=${recipe.id}`)
      .then((r) => r.json())
      .then((j) => setFull(j.meals[0]))
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    getRecipe();
  }, [recipe.id]);

  const ingredients = [];

  if (full) {
    for (let i = 1; i <= 20; i++) {
      const ing = full["strIngredient" + i];
      const mea = full["strMeasure" + i];

      if (ing && ing.trim()) ingredients.push(`${ing} = ${mea}`);
    }
  }

  useEffect(() => {
    if (full) {
      setInstructions(full.strInstructions);
      setSmallInstructions(full.strInstructions.slice(0, 200));
    }
  }, [full]);

  // const openNext = () => {
  //   const i = RECIPES.findIndex((r) => r.id === recipe.id);
  //   const next = RECIPES[i + 1] % RECIPES.length;
  //   navigation.push("RecipeDetail", { recipeId: RECIPES[next].id });
  // };

  const onShare = async () => {
    try {
      const result = await Share.share({
        title: "Sharing recipe",
        message: `Готовлю: ${recipe.name} (${recipe.area})`,
      });
    } catch (err) {}
  };

  let youtube = "";

  if (full) {
    youtube = full.strYoutube;
  }

  const onLinkYoutube = () => {
    Linking.openURL(youtube);
  };



  return (
    <ScrollView style={styles.screen}>
      <Image source={{ uri: recipe.thumb }} style={styles.image} />
      <View style={styles.body}>
        <Text style={styles.title}>{recipe.name}</Text>
        <Text style={styles.meta}>Категория: {recipe.category}</Text>
        <Text style={styles.meta}>Страна: {recipe.area}</Text>
        <Text style={styles.section}>Ингредиенты:</Text>
        {ingredients.map((t) => {
          return (
            <Text key={t} style={styles.text}>
              {t}
            </Text>
          );
        })}
        <Text style={{ marginTop: 10, color: "#C0C0C0", fontSize: 18 }}>
          Инструкции: {instructionsAreFull ? instructions : smallInstructions}
          {instructions.length < 200 ? (
            <></>
          ) : (
            <Pressable onPress={() => setInstructionsAreFull((prev) => !prev)}>
              <Text style={{ color: "#181717" }}>
                {instructionsAreFull ? "   Show less" : "...Show more"}
              </Text>
            </Pressable>
          )}
        </Text>
      </View>
      <View style={{ marginTop: 20, paddingHorizontal: 16 }}>
        <Text style={styles.section}>Похожие:</Text>
      </View>
      <FlatList
        horizontal
        data={similar}
        keyExtractor={(item) => item.id}
        contentContainerStyle={{ paddingHorizontal: 10, paddingBottom: 10 }}
        showsHorizontalScrollIndicator={false}
        renderItem={({ item }) => (
          <PressableCard
            onPress={() => {
              navigation.push("RecipeDetail", { recipe: item });
            }}
            style={{ width: 160, padding: 6 }}
          >
            <RecipeCard recipe={item} />
          </PressableCard>
        )}
      />
      <Pressable style={styles.pressable} onPress={onShare}>
        <Text>Поделиться рецептом</Text>
      </Pressable>
      {youtube ? (
        <Pressable style={styles.pressable} onPress={onLinkYoutube}>
          <Text>Видео-рецепт</Text>
        </Pressable>
      ) : (
        <></>
      )}
      <View style={{ paddingHorizontal: 16, marginTop: 10, marginBottom: 20 }}>
        <Text style={styles.section}>Личная заметка:</Text>
        <TextInput
          style={{
            borderWidth: 1,
            borderColor: "#cbd5e1",
            padding: 10,
            borderRadius: 8,
            minHeight: 80,
            textAlignVertical: "top"
          }}
          multiline
          placeholder="Напишите здесь вашу личную заметку..."
          value={text}
          onChangeText={setText}
        />
        <View style={{ marginTop: 8 }}>
          <Button title="Сохранить заметку" onPress={saveNote} />
        </View>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: "#fff" },
  image: { width: "100%", height: 240 },
  body: { padding: 16 },
  title: { fontSize: 24, fontWeight: "bold", color: "#1e293b" },
  meta: { fontSize: 14, color: "#64748b", marginTop: 4 },
  section: {
    fontSize: 18,
    fontWeight: "bold",
    marginTop: 20,
    marginBottom: 6,
    color: "#1e29eb",
  },
  text: { fontSize: 15, color: "#64748b", marginTop: 40 },
  pressable: {
    marginTop: 8,
    alignItems: "center",
    backgroundColor: "#759ddd",
    padding: 10,
  },
});

export default RecipeDetailsScreen;
