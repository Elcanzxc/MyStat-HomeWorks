import {
  FlatList,
  StyleSheet,
  Text,
  useWindowDimensions,
  View,
  Button,
} from "react-native";
import { useNavigation, useFocusEffect } from "@react-navigation/native";
import RecipeCard from "../components/RecipeCard";
import { useState, useCallback } from "react";
import PressableCard from "../components/PressableCard";
import { load, remove } from "../utils/storage";

const HistoryScreen = () => {
  const navigation = useNavigation();
  const { width, height } = useWindowDimensions();
  const numColumns = width > height ? 3 : 2;
  const [history, setHistory] = useState([]);

  useFocusEffect(
    useCallback(() => {
      load("recent", []).then(setHistory);
    }, [])
  );

  const clearHistory = async () => {
    await remove("recent");
    setHistory([]);
  };

  return (
    <View style={styles.screen}>
      <View style={{ marginBottom: 10, paddingHorizontal: 6 }}>
        <Button title="Очистить историю" onPress={clearHistory} color="#ef4444" />
      </View>
      <FlatList
        data={history}
        key={numColumns}
        numColumns={numColumns}
        keyExtractor={(item) => item.id}
        contentContainerStyle={{ padding: 6 }}
        renderItem={({ item }) => (
          <PressableCard
            onPress={() => {
              navigation.navigate("Recipes", {
                screen: "RecipeDetail",
                params: { recipe: item },
              });
            }}
            style={{ flex: 1 / numColumns, padding: 7 }}
          >
            <RecipeCard recipe={item} />
          </PressableCard>
        )}
        ListEmptyComponent={<Text style={styles.empty}>История пуста</Text>}
      />
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
  empty: { textAlign: "center", color: "#64748b", marginTop: 40 },
});

export default HistoryScreen;
