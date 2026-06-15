import { View, StyleSheet, useWindowDimensions } from "react-native";

export default function ListSeparator() {
  const { width } = useWindowDimensions();
  const widthPos = width < 600 ? 0.8 : 0.85;

  return (
    <View style={[styles.container, { width: width * widthPos, height: 1 }]} />
  );
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: "black",
    alignSelf: "center",
  },
});
