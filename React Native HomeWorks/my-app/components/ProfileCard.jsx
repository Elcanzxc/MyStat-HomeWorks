import { StyleSheet, View, Text, Image } from "react-native";

export default function ProfileCard({ name, age, selected }) {
  return (
    <View>
      <View style={[styles.card, selected && styles.selectedCard]}>
        <Image
          source={{
            uri: `https://i.pravatar.cc/150?img=${Math.floor(Math.random() * 10) + 1}`,
          }}
          style={styles.avatar}
        />
        <Text style={styles.text}>Name:{name}</Text>
        <Text style={styles.text}>Age:{age}</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {},
  card: {
    borderWidth: 1,
    width: 160,
    height: 180,
    borderColor: "#034569",
    backgroundColor: "#086CA2",
    justifyContent: "center",
    alignItems: "center",
    borderRadius: 15,
  },
  avatar: {
    width: 100,
    height: 100,
    borderRadius: 15,
  },
  text: {
    fontSize: 22,
    color: "white",
  },
  selectedCard: {
    borderColor: "yellow",
    borderWidth: 4,
  },
});
