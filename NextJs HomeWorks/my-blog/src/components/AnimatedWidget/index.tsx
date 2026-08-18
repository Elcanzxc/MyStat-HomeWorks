'use client'
import React, {useEffect,useRef, useState } from "react";

export default function AnimatedWidget({ children, title }: { children: React.ReactNode, title: string }) {
  const [isMounted, setIsMounted] = useState(false);
  
  useEffect(() => {
    setIsMounted(true);
  }, []);
  return (
    <div className={`transition-all duration-700 transform ${
        isMounted ? "opacity-100 translate-y-0" : "opacity-0 translate-y-10"
    }`}>
      <h3 className="text-xl font-bold mb-4">{title}</h3>
      {children} 
    </div>
  );
}