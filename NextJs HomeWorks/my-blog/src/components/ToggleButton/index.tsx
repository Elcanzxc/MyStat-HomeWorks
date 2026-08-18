'use client'
import { createContext, ReactNode, useContext, useState } from "react";

export default function ToggleButton() {
  const context = useTheme()

  return (
    <button 
      onClick={context.toggleTheme}
      className="px-3 py-1.5 text-sm font-medium text-slate-700 dark:text-slate-300 bg-slate-100 dark:bg-slate-800 hover:bg-slate-200 dark:hover:bg-slate-700 rounded-md transition-colors flex items-center gap-1.5"
    >
      {context.theme === 'light' ? '🌙 Тёмная' : '☀️ Светлая'}
    </button>
  )
}



type ThemeContextValue = {
  theme: "light" | "dark";
  toggleTheme: () => void;
};

export const ThemeContext = createContext<ThemeContextValue | null>(null);

export function ThemeProvider({ children }: { children: ReactNode }) {
  const [theme, setTheme] = useState<"light" | "dark">("light");

  function toggleTheme() {
    setTheme((prev) => (prev === "light" ? "dark" : "light"));
    document.documentElement.classList.toggle('dark')
  }

  return (
    <ThemeContext.Provider value={{ theme, toggleTheme }}>
      {children}
    </ThemeContext.Provider>
  );
}

export function useTheme() {
  const context = useContext(ThemeContext);
    if (!context) {
    throw new Error("useTheme must be used inside ThemeProvider");
  }
  return context;
}
