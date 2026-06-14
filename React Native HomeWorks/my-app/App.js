import { View, StyleSheet, Text, ScrollView, FlatList, Pressable } from "react-native";
import ProfileCard from "./components/ProfileCard";
import ProfileCardHeader from "./components/ProfileCardHeader";
import ProfileCardSeparator from "./components/ProfileCardSeparator";
import ProfileCardEmpty from "./components/ProfileCardEmpty";
import { useState } from "react";
function App() {
  const users = [
    { name: "Elcan", age: 20 },
    { name: "Aqsin", age: 19 },
    { name: "Salman", age: 19 },
    { name: "Turan", age: 19 },
    { name: "Elbrus", age: 19 },

    { name: "Elcan", age: 20 },
    { name: "Aqsin", age: 19 },
    { name: "Salman", age: 19 },
    { name: "Turan", age: 19 },
    { name: "Elbrus", age: 19 },

    { name: "Elcan", age: 20 },
    { name: "Aqsin", age: 19 },
    { name: "Salman", age: 19 },
    { name: "Turan", age: 19 },
    { name: "Elbrus", age: 19 },
  ];


  const [selectedId,setSelectedId] = useState(null)

  return (
    <View style={styles.container}>
      <FlatList
        ListHeaderComponent={
          <View style={styles.header}>
            <ProfileCardHeader />
          </View>
        }
        ListEmptyComponent={
          <View style={styles.header}>
            <ProfileCardEmpty />
          </View>
        }
        ItemSeparatorComponent={<ProfileCardSeparator />}
        contentContainerStyle={styles.flatlistElements}
        data={users}
        numColumns={2}
        columnWrapperStyle={styles.card}
        renderItem={({ item: { name, age }, index }) => (
          <Pressable onPress={() =>{ setSelectedId(index)}}>
           
          <View>
            <ProfileCard name={name} age={age} selected={selectedId ===index}/>
          </View>
          </Pressable>
        )}
        keyExtractor={(item, index) => index.toString()}
      ></FlatList>
       {console.log(`vibral id:${selectedId}`)}
    </View>
  );
}

export default App;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#64AAD0",
    paddingTop:50
  },
  flatlistElements: {
    padding: 16,
    rowGap: 20,
  },
  card: {
    justifyContent: "space-evenly",
  },
  header: {
    alignItems: "center",
  },
});
