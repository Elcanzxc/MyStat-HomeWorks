import { useState } from "react";
import {
  View,
  Text,
  Image,
  StyleSheet,
  Platform,
  Pressable,
} from "react-native";
import { useFavorites } from "../../context/FavoritesContext";

const RecipeCard = ({ recipe }) => {
    const { isFavorit, toggleFavorite } = useFavorites();
  
    return (
      <View style={styles.card}>
        <Image source={{ uri: recipe.thumb }} style={styles.image} />
  
        <View style={styles.info}>
          <Text style={styles.title} numberOfLines={1}>
            {recipe.name}
          </Text>
          <Text style={styles.category}>{recipe.category}</Text>
          <Text style={styles.area}>{recipe.area}</Text>
        </View>
  
        <Pressable
          style={styles.heart}
          onPress={() => toggleFavorite(recipe.id)}
        >
          <Text style={styles.favorites}>
            {isFavorit(recipe.id) ? "❤️" : "🤍"}
          </Text>
        </Pressable>
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
  area: {
    position: "absolute",
    top: 10,
    right: 5,
    backgroundColor: "green",
    padding: 5,
    color: "white",
    borderRadius: 10,
  },
  title: { fontSize: 15, fontWeight: "bold", color: "#1e293b" },
  category: { fontSize: 12, color: "#64748b", marginTop: 2 },
  favorites:{fontSize:20 },
  heart: {position:'absolute',top:5,left:5}
});

export default RecipeCard;
