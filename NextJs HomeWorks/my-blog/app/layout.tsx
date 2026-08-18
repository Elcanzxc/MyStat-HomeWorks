import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import ForwardButton from "@/src/components/ForwardButton";
import BackButton from "@/src/components/BackButton";
import ToggleButton, { ThemeProvider } from "@/src/components/ToggleButton";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Elcan`s Blog",
  description: "Blog created by Elcan",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html
      lang="en"
      className={`${geistSans.variable} ${geistMono.variable} h-full antialiased`}
    >
      <body className="min-h-screen bg-slate-50 dark:bg-slate-900 text-slate-900 dark:text-slate-100 flex flex-col font-sans selection:bg-blue-200 dark:selection:bg-blue-900">
        <ThemeProvider>
          <header className="bg-white dark:bg-slate-950 shadow-sm sticky top-0 z-10">
            <div className="max-w-4xl mx-auto px-4 py-4 flex items-center justify-between">
              <h1 className="text-xl font-bold tracking-tight text-blue-600 dark:text-blue-400">
                <a href="/">Elcan's Blog</a>
              </h1>
              <div className="flex gap-2">
                <ToggleButton />
                <BackButton />
                <ForwardButton />
              </div>
            </div>
          </header>

          <main className="grow max-w-4xl w-full mx-auto px-4 py-8">
            {children}
          </main>

          <footer className="bg-slate-100 dark:bg-slate-950 border-t border-slate-200 dark:border-slate-800 py-6 mt-auto">
            <div className="max-w-4xl mx-auto px-4 text-center text-slate-500 dark:text-slate-400 text-sm">
              &copy; {new Date().getFullYear()} Elcan's Blog. Все права
              защищены.
            </div>
          </footer>
        </ThemeProvider>
      </body>
    </html>
  );
}
