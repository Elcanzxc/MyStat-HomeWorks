import { Image, ScrollView, StyleSheet, Text, View, Pressable } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { useRoute } from "@react-navigation/native";
import { RECIPES } from "../data/recipes";

const RecipeDetailScreen = () => {
  const { recipe } = useRoute().params;
  const navigation = useNavigation();
  const openNext = () => {
    const i = RECIPES.findIndex((r) => r.id === recipe.id);
    const next = RECIPES[(i + 1) % RECIPES.length];
    navigation.push("RecipeDetail", { recipe: next });
  };

  // useEffect(() => {
  //     navigation.setOptions({ title: recipe.name })
  // }, [recipe])

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
          onPress={() => navigation.push("CategoryList", { category: recipe.category })}
        >
          <Text style={styles.categoryButtonText}>Еще из этой категории</Text>
        </Pressable>
        <Text style={styles.section}>Ингредиенты</Text>
        <Text style={styles.text}>
          * Заглушка - настоящие ингредиенты придут с API в Модуле 5
        </Text>
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
