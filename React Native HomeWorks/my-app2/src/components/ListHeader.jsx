import { View, StyleSheet, useWindowDimensions, Text } from "react-native";

export default function ListHeader({ text, orientation }) {
  const { width, height } = useWindowDimensions();

  const screenWidth = Math.round(width);
  const screenHeight = Math.round(height);

  const widthPos = width < 600 ? 0.8 : 0.4;
  const heightPos = height > 600 ? 8 : 4;

  return (
    <View
      style={[
        styles.container,
        { width: width * widthPos, minHeight: height / heightPos },
      ]}
    >
      <Text style={styles.title}>{text}</Text>

      <Text style={styles.subtitle}>
        Ekran: {screenWidth}x{screenHeight}
      </Text>
      <Text style={styles.subtitle}>Orentation: {orientation}</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: "#ACC3A6",
    borderRadius: 30,
    borderColor: "#2D080A",
    borderWidth: 1,
    alignItems: "center",
    justifyContent: "center",
    padding: 15,
  },
  title: {
    fontSize: 26,
    color: "#553e38",
    fontWeight: "bold",
    marginBottom: 5,
  },
  subtitle: {
    fontSize: 16,
    color: "#333",
  },
});
