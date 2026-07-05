import { useContext } from "react";
import { View, Text } from "react-native";
import { FavoritesContext, useFavorites } from "../context/FavoritesContext";
import { RECIPES } from "../data/recipes";
import { FlatList } from "react-native";
import RecipeCard from "../components/RecipeCard";
import PressableCard from "../components/PressableCard";
import { useNavigation } from "@react-navigation/native";
const FavoriteScreen = () => {
  const { favorites } = useFavorites();

  const navigation = useNavigation();

  const list = RECIPES.filter((r) => favorites.includes(r.id));

  return (
    <FlatList
      data={list}
      keyExtractor={(item) => item.id}
      renderItem={({ item }) => (
        <PressableCard
          onPress={() => navigation.navigate("Recipes", { screen: "RecipeDetail", params: { recipe: item } })}
        >
          <RecipeCard recipe={item} />
        </PressableCard>
      )}
      ListEmptyComponent={<Text> Пока пусто</Text>}
      contentContainerStyle={{ padding: 6, gap: 20 }}
    />
  );
};

export default FavoriteScreen;
