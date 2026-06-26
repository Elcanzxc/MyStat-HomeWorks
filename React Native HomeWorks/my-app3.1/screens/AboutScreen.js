import { View, Text, StyleSheet } from "react-native";

const AboutScreen = () => {
  return (
    <View style={styles.screen}>
      <Text style={styles.title}>О приложении</Text>
      <Text style={styles.text}>Книга рецептов</Text>
      <Text style={styles.text}>Версия: 1</Text>
      <Text style={styles.text}>Разработано Elcan Aliyev</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  screen: {
    flex: 1,
    padding: 20,
    backgroundColor: "#F4F7FA",
    alignItems: "center",
    justifyContent: "center",
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 20,
    color: "#1e293b",
  },
  text: {
    fontSize: 16,
    color: "#475569",
    marginBottom: 10,
    textAlign: "center",
  },
});

export default AboutScreen;
