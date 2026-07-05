import {
  Image,
  ScrollView,
  StyleSheet,
  Text,
  View,
  Pressable,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { useRoute } from "@react-navigation/native";
import { RECIPES } from "../data/recipes";
import { useEffect ,useState} from "react";

const RecipeDetailScreen = () => {
  const { recipe } = useRoute().params;
  const navigation = useNavigation();
  const openNext = () => {
    const i = RECIPES.findIndex((r) => r.id === recipe.id);
    const next = RECIPES[(i + 1) % RECIPES.length];
    navigation.push("RecipeDetail", { recipe: next });
  };
 const [full, setFull] = useState(null);
  const getRecipe = async () => {
fetch(`https://www.themealdb.com/api/json/v1/1/lookup.php?i=${recipe.id}`)
  .then((r) => r.json())
  .then((j) => setFull(j.meals[0]))
  .catch(console.log);
  };

  useEffect(() => {
    getRecipe();
  }, [recipe.id]);

  const ingredients = [];

  if (full) {
    for (let i = 1; i <= 20; i++) {
      const ing = full["strIngredient" + i];
      const mea = full["strMeasure" + i];

      if (ing && ing.trim()) ingredients.push(`${ing} - ${mea}`);
    }
  }

  return (
    <ScrollView style={styles.screen}>
      <Image source={{ uri: recipe.thumb }} style={styles.image} />
      <View style={styles.body}>
        <Text style={styles.title}>{recipe.name}</Text>
        <Text style={styles.meta}>
          {recipe.category} * {recipe.area}
        </Text>
        <Pressable
          style={styles.categoryButton}
          onPress={() =>
            navigation.push("CategoryList", { category: recipe.category })
          }
        >
          <Text style={styles.categoryButtonText}>Еще из этой категории</Text>
        </Pressable>
        <Text style={styles.section}>Ингредиенты</Text>
        {ingredients.map((t) => {
          return <Text key={t} style={styles.text}>{t}</Text>;
        })}
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
  categoryButton: {
    marginTop: 12,
    paddingVertical: 8,
    paddingHorizontal: 12,
    backgroundColor: "#e2e8f0",
    borderRadius: 8,
    alignSelf: "flex-start",
  },
  categoryButtonText: {
    color: "#0f172a",
    fontWeight: "600",
    fontSize: 14,
  },
  section: {
    fontSize: 18,
    fontWeight: "bold",
    marginTop: 20,
    marginBottom: 6,
    color: "#1e293b",
  },
  text: { fontSize: 15, color: "#334155", lineHeight: 22 },
});

export default RecipeDetailScreen;
