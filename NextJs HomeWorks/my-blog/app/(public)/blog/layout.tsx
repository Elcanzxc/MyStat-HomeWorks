import React, { ReactNode } from "react";

export default function Layout({ children }: { children: ReactNode }) {
  return (
    <div className="bg-white dark:bg-slate-900 rounded-2xl shadow-sm border border-slate-100 dark:border-slate-800 p-6 md:p-8">
      <div className="mb-8 border-b border-slate-100 dark:border-slate-800 pb-4">
        <h2 className="text-sm font-semibold text-blue-600 dark:text-blue-400 uppercase tracking-wider">
          Блоги Elcan'а
        </h2>
      </div>
      <div className="prose prose-slate dark:prose-invert max-w-none">
        {children}
      </div>
    </div>
  );
}
