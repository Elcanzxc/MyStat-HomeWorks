import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

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
      <body className="min-h-full flex flex-col">{children}</body>
    </html>
  );
}
