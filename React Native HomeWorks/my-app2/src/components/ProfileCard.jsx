import { useState } from "react";
import {
  View,
  StyleSheet,
  useWindowDimensions,
  Image,
  Text,
  Pressable,
} from "react-native";

export default function ProfileCard({
  name,
  role,
  avatar,
  isOnline,
  selected,
  onPress,
}) {
  const { width, height } = useWindowDimensions();

  const widthPos = width < 600 ? 0.8 : 0.4;
  const heightPos = height > 600 ? 9 : 4;

  return (
    <Pressable onPress={onPress}>
      <View
        style={[
          styles.container,
          { width: width * widthPos, minHeight: height / heightPos },
          selected && styles.pressed,
        ]}
      >
        <View style={styles.avatarContainer}>
          <Image source={{ uri: avatar }} style={styles.avatar} />
          {isOnline && <View style={styles.onlineDot} />}
        </View>

        <View style={styles.infoContainer}>
          <Text style={styles.nameText} numberOfLines={1}>
            {name}
          </Text>
          <Text style={styles.roleText}>{role}</Text>
        </View>

        <View style={styles.badgeContainer}>
          <View style={styles.badge}>
            <Text style={styles.badgeText}>Badge</Text>
          </View>
        </View>
      </View>
    </Pressable>
  );
}
// avatarContainer: {
//   flex: 1,
// },

const styles = StyleSheet.create({
  container: {
    backgroundColor: "#ACC3A6",
    flexDirection: "row",
    borderRadius: 30,
    borderColor: "#2D080A",
    borderWidth: 1,
    padding: 10,
    alignItems: "center",
  },
  avatarContainer: {
    width: 60,
    height: 60,
    marginRight: 10,
  },
  avatar: {
    width: "100%",
    height: "100%",
    borderRadius: 30,
  },
  onlineDot: {
    width: 16,
    height: 16,
    borderRadius: 8,
    backgroundColor: "#3aaf6b",
    position: "absolute",
    bottom: 0,
    right: 0,
    borderWidth: 2,
    borderColor: "#ACC3A6",
  },
  check: {
    flex: 1,
    backgroundColor: "red",
  },
  check2: {
    flex: 1,
    backgroundColor: "yellow",
  },
  infoContainer: {
    flex: 1,
    justifyContent: "center",
  },
  nameText: {
    fontSize: 20,
    color: "#333",
    fontWeight: "bold",
  },
  roleText: {
    fontSize: 14,
    color: "#555",
    marginTop: 2,
  },
  badgeContainer: {
    marginLeft: 10,
    justifyContent: "center",
    alignItems: "center",
  },
  badge: {
    backgroundColor: "#2D080A",
    paddingVertical: 4,
    paddingHorizontal: 8,
    borderRadius: 12,
  },
  badgeText: {
    color: "white",
    fontSize: 12,
    fontWeight: "bold",
  },
  pressed: {
    borderColor: "#c4db2f",
    borderWidth: 3,
  },
});
