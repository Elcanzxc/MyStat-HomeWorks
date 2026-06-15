import {
  FlatList,
  StyleSheet,
  View,
  useWindowDimensions,
  Button,
} from "react-native";
import ProfileCard from "./src/components/ProfileCard";
import ListHeader from "./src/components/ListHeader";
import ListSeparator from "./src/components/ListSeparator";
import { useState } from "react";

export default function App() {
  const { width, height } = useWindowDimensions();

  const isLandscape = width > height;
  const numColumns = isLandscape ? 2 : 1;
  const orientationText = isLandscape ? "Albom" : "Landscape";

  const [selectedIndex, setSelectedIndex] = useState(null);
  const [refreshing, setRefreshing] = useState(false);

  const onRefresh = () => {
    setRefreshing(true);
    setTimeout(() => {
      setRefreshing(false);
    }, 2000);
  };

  const [users, setUsers] = useState([
    {
      id: "1",
      name: "Elcan",
      role: "Backend",
      avatar: "https://i.pravatar.cc/150?img=11",
      isOnline: true,
    },
    {
      id: "2",
      name: "Mehemmed",
      role: "Frontend",
      avatar: "https://i.pravatar.cc/150?img=12",
      isOnline: false,
    },
    {
      id: "3",
      name: "Elbrus",
      role: "Dizayn",
      avatar: "https://i.pravatar.cc/150?img=5",
      isOnline: true,
    },
    {
      id: "4",
      name: "Bayramov",
      role: "DevOps",
      avatar: "https://i.pravatar.cc/150?img=14",
      isOnline: false,
    },
    {
      id: "5",
      name: "Aqsin",
      role: "Xz",
      avatar: "https://i.pravatar.cc/150?img=9",
      isOnline: true,
    },
    {
      id: "6",
      name: "Mahir",
      role: "Fullstack",
      avatar: "https://i.pravatar.cc/150?img=15",
      isOnline: true,
    },
  ]);

  const deleteSelected = () => {
    if (selectedIndex === null) return;
    setUsers((prev) => prev.filter((_, index) => index !== selectedIndex));
    setSelectedIndex(null);
  };

  return (
    <View style={styles.container}>
      {selectedIndex !== null && (
        <Button title="Delete Selected" onPress={deleteSelected} />
      )}

      <FlatList
        ListHeaderComponent={
          <View style={styles.listHeaderComponent}>
            <ListHeader text="Our Team" orientation={orientationText} />
          </View>
        }
        ListEmptyComponent={
          <View style={styles.listHeaderComponent}>
            <ListHeader text="Pustoy spisok" orientation={orientationText} />
          </View>
        }
        ItemSeparatorComponent={() => (
          <View style={{ marginVertical: 15 }}>
            <ListSeparator />
          </View>
        )}
        refreshing={refreshing}
        onRefresh={onRefresh}
        data={users}
        key={numColumns}
        columnWrapperStyle={
          numColumns > 1 ? { gap: 20, marginVertical: 10 } : undefined
        }
        contentContainerStyle={styles.flatlistcontainer}
        keyExtractor={(item) => item.id}
        numColumns={numColumns}
        renderItem={({ item, index }) => (
          <ProfileCard
            name={item.name}
            role={item.role}
            avatar={item.avatar}
            isOnline={item.isOnline}
            selected={selectedIndex === index}
            onPress={() =>
              setSelectedIndex(selectedIndex === index ? null : index)
            }
          />
        )}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    alignItems: "center",
    justifyContent: "center",
    flex: 1,
    backgroundColor: "#F3D9DC",
    paddingTop: 60,
    paddingHorizontal: 20,
  },
  flatlistcontainer: {
    paddingBottom: 20,
  },
  listHeaderComponent: {
    alignItems: "center",
    marginBottom: 20,
  },
});
