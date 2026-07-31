import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import ForwardButton from "@/src/components/ForwardButton";
import BackButton from "@/src/components/BackButton";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
<<<<<<<< HEAD:NextJs/my-blog/src/app/layout.tsx
  title: "Elcan Blog",
  description: "Никогда не поздно никогда не рано",
========
  title: "Elcan`s Blog",
  description: "Blog created by Elcan",
>>>>>>>> 93eb867 (Next.js Starter):NextJs/my-blog/app/layout.tsx
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
      <body className="min-h-screen bg-slate-50 text-slate-900 flex flex-col font-sans selection:bg-blue-200">
        <header className="bg-white shadow-sm sticky top-0 z-10">
          <div className="max-w-4xl mx-auto px-4 py-4 flex items-center justify-between">
            <h1 className="text-xl font-bold tracking-tight text-blue-600">
              <a href="/">Elcan's Blog</a>
            </h1>
            <div className="flex gap-2">
              <BackButton />
              <ForwardButton />
            </div>
          </div>
        </header>
        
        <main className="flex-grow max-w-4xl w-full mx-auto px-4 py-8">
          {children}
        </main>

        <footer className="bg-slate-100 border-t border-slate-200 py-6 mt-auto">
          <div className="max-w-4xl mx-auto px-4 text-center text-slate-500 text-sm">
            &copy; {new Date().getFullYear()} Elcan's Blog. Все права защищены.
          </div>
        </footer>
      </body>
    </html>
  );
}
