import { createContext, useCallback, useContext, useState } from "react";

export const FavoritesContext = createContext();

export const FavoritesProvider = ({ children }) => {
  const [favorites, setFavorites] = useState([]);

  const toggleFavorite = (id) => {
    setFavorites((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    );
  };
  const isFavorit = (id) => favorites.includes(id);

  return (
    <FavoritesContext.Provider value={{ favorites, toggleFavorite, isFavorit }}>
      {children}
    </FavoritesContext.Provider>
  );
};

export const useFavorites = () => useContext(FavoritesContext);
