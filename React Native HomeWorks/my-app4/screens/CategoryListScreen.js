import { FlatList, View, StyleSheet, Text, useWindowDimensions } from "react-native";
import { useNavigation, useRoute } from "@react-navigation/native";
import { RECIPES } from "../data/recipes";
import RecipeCard from "../components/RecipeCard";
import PressableCard from "../components/PressableCard";

const CategoryListScreen = () => {
  const route = useRoute();
  const navigation = useNavigation();
  const { category } = route.params;
  const { width, height } = useWindowDimensions();
  const numColumns = width > height ? 3 : 2;

  const filtered = RECIPES.filter((item) => item.category === category);

  return (
    <View style={styles.screen}>
      <FlatList
        data={filtered}
        key={numColumns}
        numColumns={numColumns}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={{ padding: 6 }}
        renderItem={({ item }) => (
          <PressableCard
            onPress={() => {
              navigation.push("RecipeDetail", { recipe: item });
            }}
            style={{ flex: 1 / numColumns, padding: 6 }}
          >
            <RecipeCard recipe={item} />
          </PressableCard>
        )}
        ListEmptyComponent={<Text style={styles.empty}>Ничего не найдено</Text>}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  screen: {
    flex: 1,
    paddingTop: 8,
    paddingHorizontal: 8,
    backgroundColor: "#F4F7FA",
  },
  empty: { textAlign: "center", color: "#64748b", marginTop: 40 },
});

export default CategoryListScreen;
