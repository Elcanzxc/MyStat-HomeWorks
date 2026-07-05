import {
  View,
  Text,
  StyleSheet,
  Image,
  Platform,
  Pressable,
} from "react-native";
import { useFavorites } from "../../context/FavoritesContext";
import { useSQLiteContext } from "expo-sqlite";
import { useEffect, useState, useCallback } from "react";
import { useFocusEffect } from "@react-navigation/native";

const RecipeCard = ({ recipe }) => {
  const db = useSQLiteContext()
  const [count, setCount] = useState(0)
  const { isFavorite, toggleFavorite } = useFavorites();

  async function getCount() {
    const row = await db.getFirstAsync(
      `SELECT count FROM openCounts WHERE recipeId = ?`,
      recipe.id,
    );

    row ? setCount(row.count) : setCount(0)
  }

  useFocusEffect(
    useCallback(() => {
      getCount();
    }, [recipe.id])
  );

  return (
    <View style={styles.card}>
      <Image style={styles.image} source={{ uri: recipe.thumb }} />
      <View style={styles.info}>
        <Text style={styles.title} numberOfLines={1}>
          {recipe.name}
        </Text>
        <Text style={styles.category}>{recipe.category}</Text>
        <Text style={styles.area}>{recipe.area ?? "unknown"}</Text>
        <Text style={styles.count}>👁️ {count ? count : 0}</Text>
        <Pressable
          style={styles.pressableHearts}
          onPress={() => toggleFavorite(recipe)}
        >
          <Text style={{ fontSize: 20 }}>
            {isFavorite(recipe.id) ? "❤️" : "🤍"}
          </Text>
        </Pressable>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  card: {
    backgroundColor: "#fff",
    borderRadius: 12,
    overflow: "hidden",
    ...Platform.select({
      ios: {
        shadowColor: "#000",
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.12,
        shadowRadius: 5,
      },
      android: { elevation: 3 },
    }),
  },
  image: {
    width: "100%",
    aspectRatio: 1,
  },
  info: {
    position: "static",
    padding: 10,
  },
  title: {
    fontSize: 15,
    fontWeight: "bold",
    color: "#1e293b",
  },
  category: {
    fontSize: 12,
    color: "64748b",
    marginTop: 2,
  },
  area: {
    fontSize: 10,
    color: "64728b",
    marginTop: 2,
    backgroundColor: "green",
    padding: 4,
    position: "absolute",
    top: 8,
    right: 4,
    borderRadius: 4,
  },
  pressableHearts: {
    position: "absolute",
    top: 4,
    left: 4,
  },
  count: {
    fontSize: 10,
    color: "#ffffff",
    marginTop: 2,
    backgroundColor: "green",
    padding: 4,
    position: "absolute",
    bottom: 8,
    right: 4,
    borderRadius: 4,
  }
});

export default RecipeCard;
