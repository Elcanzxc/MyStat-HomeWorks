module.exports = [
"[externals]/next/dist/shared/lib/no-fallback-error.external.js [external] (next/dist/shared/lib/no-fallback-error.external.js, cjs)", ((__turbopack_context__, module, exports) => {

const mod = __turbopack_context__.x("next/dist/shared/lib/no-fallback-error.external.js", () => require("next/dist/shared/lib/no-fallback-error.external.js"));

module.exports = mod;
}),
"[project]/app/favicon.ico (static in ecmascript, tag client)", ((__turbopack_context__) => {

__turbopack_context__.v("/_next/static/media/favicon.2vob68tjqpejf.ico" + (globalThis["NEXT_CLIENT_ASSET_SUFFIX"] || ''));}),
"[project]/app/favicon.ico.mjs { IMAGE => \"[project]/app/favicon.ico (static in ecmascript, tag client)\" } [app-rsc] (structured image object, ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "default",
    ()=>__TURBOPACK__default__export__
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$app$2f$favicon$2e$ico__$28$static__in__ecmascript$2c$__tag__client$29$__ = __turbopack_context__.i("[project]/app/favicon.ico (static in ecmascript, tag client)");
;
const __TURBOPACK__default__export__ = {
    src: __TURBOPACK__imported__module__$5b$project$5d2f$app$2f$favicon$2e$ico__$28$static__in__ecmascript$2c$__tag__client$29$__["default"],
    width: 256,
    height: 256
};
}),
"[project]/src/utils/index.ts [app-rsc] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "articles",
    ()=>articles,
    "delay",
    ()=>delay
]);
async function delay(ms) {
    return new Promise((resolve)=>setTimeout(resolve, ms));
}
const articles = [
    {
        id: 1,
        title: "Как современные технологии ускоряют разработку",
        slug: "modern-technologies-development",
        description: "Почему современные технологии позволяют создавать проекты быстрее, чем несколько лет назад.",
        content: "Еще недавно настройка нового проекта могла занимать несколько часов, а сегодня большинство задач решается буквально за несколько минут. Готовые инструменты, удобные фреймворки и автоматизация помогают разработчикам сосредоточиться на создании продукта, а не на бесконечной настройке окружения.",
        author: "Иван Иванов",
        category: "Технологии",
        tags: [
            "JavaScript",
            "Frontend"
        ],
        image: "https://picsum.photos/800/400?random=1",
        createdAt: "2026-07-29T10:00:00Z"
    },
    {
        id: 2,
        title: "Что делает интерфейс действительно удобным",
        slug: "what-makes-good-ui",
        description: "Хороший дизайн — это не только красивые цвета, но и удобство для пользователя.",
        content: "Когда интерфейс продуман, пользователь практически не замечает его существования. Все нужные действия выполняются интуитивно, страницы загружаются быстро, а элементы находятся именно там, где их ожидаешь увидеть. Именно такие детали формируют положительный опыт использования.",
        author: "Анна Смирнова",
        category: "Дизайн",
        tags: [
            "UI",
            "UX"
        ],
        image: "https://picsum.photos/800/400?random=2",
        createdAt: "2026-07-28T14:30:00Z"
    },
    {
        id: 3,
        title: "Главные события из мира IT за неделю",
        slug: "weekly-it-news",
        description: "Подводим итоги недели и рассказываем о самых интересных новостях из мира IT.",
        content: "За прошедшую неделю вышло несколько заметных обновлений популярных библиотек, появились новые инструменты для разработки, а также состоялись анонсы, которые уже активно обсуждаются в профессиональном сообществе. Собрали самое интересное в одном месте.",
        author: "Петр Петров",
        category: "Новости",
        tags: [
            "Обновления"
        ],
        image: "https://picsum.photos/800/400?random=3",
        createdAt: "2026-07-27T09:15:00Z"
    },
    {
        id: 4,
        title: "React в 2026 году",
        slug: "react-in-2026",
        description: "Что изменилось в экосистеме React за последнее время.",
        content: "React продолжает оставаться одним из самых популярных инструментов для создания интерфейсов. Новые возможности делают разработку быстрее, а экосистема становится еще богаче.",
        author: "Алексей Козлов",
        category: "Frontend",
        tags: [
            "React",
            "JavaScript"
        ],
        image: "https://picsum.photos/800/400?random=4",
        createdAt: "2026-07-26T16:20:00Z"
    },
    {
        id: 5,
        title: "Почему TypeScript стоит изучить",
        slug: "why-typescript",
        description: "Несколько причин добавить TypeScript в свой стек.",
        content: "Статическая типизация помогает находить ошибки еще до запуска приложения. Это особенно полезно в крупных проектах, где кодовая база постоянно растет.",
        author: "Мария Орлова",
        category: "Программирование",
        tags: [
            "TypeScript",
            "JavaScript"
        ],
        image: "https://picsum.photos/800/400?random=5",
        createdAt: "2026-07-25T11:40:00Z"
    },
    {
        id: 6,
        title: "Пять советов по оптимизации сайта",
        slug: "website-performance",
        description: "Небольшие изменения, которые могут заметно ускорить загрузку страниц.",
        content: "Оптимизация изображений, разделение кода, кеширование и правильная работа со шрифтами позволяют улучшить производительность без серьезной переработки проекта.",
        author: "Дмитрий Волков",
        category: "Веб-разработка",
        tags: [
            "Performance",
            "Web"
        ],
        image: "https://picsum.photos/800/400?random=6",
        createdAt: "2026-07-24T08:10:00Z"
    },
    {
        id: 7,
        title: "Искусственный интеллект в повседневной работе",
        slug: "ai-everyday",
        description: "Как ИИ помогает разработчикам и дизайнерам каждый день.",
        content: "Сегодня ИИ уже используется не только для генерации текста. Он помогает искать ошибки, писать документацию, создавать макеты и автоматизировать множество рутинных процессов.",
        author: "Екатерина Белова",
        category: "ИИ",
        tags: [
            "AI",
            "Automation"
        ],
        image: "https://picsum.photos/800/400?random=7",
        createdAt: "2026-07-23T13:50:00Z"
    },
    {
        id: 8,
        title: "Минимализм в интерфейсах",
        slug: "minimal-ui",
        description: "Почему меньше иногда действительно лучше.",
        content: "Минималистичный интерфейс помогает пользователю быстрее сосредоточиться на главном. Главное — не убрать важные элементы в погоне за красивой картинкой.",
        author: "Ольга Морозова",
        category: "Дизайн",
        tags: [
            "UI",
            "Minimalism"
        ],
        image: "https://picsum.photos/800/400?random=8",
        createdAt: "2026-07-22T17:15:00Z"
    },
    {
        id: 9,
        title: "Как начать изучать Node.js",
        slug: "start-nodejs",
        description: "Небольшой план для тех, кто только начинает.",
        content: "Начните с понимания основ JavaScript, затем познакомьтесь с npm, файловой системой и созданием простого HTTP-сервера. После этого можно переходить к Express и работе с базами данных.",
        author: "Сергей Николаев",
        category: "Backend",
        tags: [
            "Node.js",
            "Backend"
        ],
        image: "https://picsum.photos/800/400?random=9",
        createdAt: "2026-07-21T09:30:00Z"
    },
    {
        id: 10,
        title: "Будущее веб-разработки",
        slug: "future-web-development",
        description: "Какие технологии будут определять развитие веба в ближайшие годы.",
        content: "Развитие браузеров, серверных компонентов, искусственного интеллекта и новых инструментов сборки делает веб-разработку быстрее и интереснее. Вероятно, в ближайшие годы автоматизации станет еще больше, а разработчики смогут уделять больше внимания логике и пользовательскому опыту.",
        author: "Никита Соколов",
        category: "Технологии",
        tags: [
            "Web",
            "Future"
        ],
        image: "https://picsum.photos/800/400?random=10",
        createdAt: "2026-07-20T12:45:00Z"
    }
];
}),
"[project]/app/(public)/blog/[id]/SameArticles.tsx [app-rsc] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "default",
    ()=>SameArticles
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/server/route-modules/app-page/vendored/rsc/react-jsx-dev-runtime.js [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$utils$2f$index$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/utils/index.ts [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$client$2f$app$2d$dir$2f$link$2e$react$2d$server$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/client/app-dir/link.react-server.js [app-rsc] (ecmascript)");
;
;
;
async function SameArticles({ article }) {
    const sameArticles = __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$utils$2f$index$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["articles"].filter((item)=>item.id !== article.id && item.tags.some((tag)=>article.tags.includes(tag)));
    await (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$utils$2f$index$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["delay"])(2000);
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
        className: "mt-12 pt-8 border-t border-slate-200",
        children: [
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("h2", {
                className: "text-2xl font-bold text-slate-800 mb-4",
                children: "Похожие по тегам"
            }, void 0, false, {
                fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                lineNumber: 22,
                columnNumber: 7
            }, this),
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "flex flex-wrap gap-2 mb-6",
                children: article.tags.map((item)=>/*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                        className: "bg-blue-100 text-blue-700 px-3 py-1 rounded-full text-sm font-medium",
                        children: [
                            "#",
                            item
                        ]
                    }, item, true, {
                        fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                        lineNumber: 25,
                        columnNumber: 12
                    }, this))
            }, void 0, false, {
                fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                lineNumber: 23,
                columnNumber: 8
            }, this),
            sameArticles.length === 0 ? /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "text-slate-500 italic bg-slate-50 p-4 rounded-lg",
                children: "нет таких"
            }, void 0, false, {
                fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                lineNumber: 32,
                columnNumber: 10
            }, this) : /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "grid gap-4 sm:grid-cols-2",
                children: sameArticles.map((item)=>/*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "group relative bg-white border border-slate-200 rounded-xl p-5 hover:shadow-md hover:border-blue-200 transition-all",
                        children: [
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                className: "text-xs font-semibold text-blue-600 bg-blue-50 px-2.5 py-1 rounded-md mb-2 inline-block",
                                children: [
                                    "Запись #",
                                    item.id
                                ]
                            }, void 0, true, {
                                fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                                lineNumber: 37,
                                columnNumber: 18
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$client$2f$app$2d$dir$2f$link$2e$react$2d$server$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["default"], {
                                href: `${item.id}`,
                                className: "focus:outline-none block mt-2",
                                children: [
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                        className: "absolute inset-0",
                                        "aria-hidden": "true"
                                    }, void 0, false, {
                                        fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                                        lineNumber: 39,
                                        columnNumber: 22
                                    }, this),
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("h3", {
                                        className: "text-lg font-bold text-slate-900 group-hover:text-blue-600 transition-colors mb-2 line-clamp-1",
                                        children: item.title
                                    }, void 0, false, {
                                        fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                                        lineNumber: 40,
                                        columnNumber: 22
                                    }, this),
                                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                                        className: "text-sm text-slate-600 line-clamp-2",
                                        children: item.content
                                    }, void 0, false, {
                                        fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                                        lineNumber: 41,
                                        columnNumber: 22
                                    }, this)
                                ]
                            }, void 0, true, {
                                fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                                lineNumber: 38,
                                columnNumber: 18
                            }, this)
                        ]
                    }, item.id, true, {
                        fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                        lineNumber: 36,
                        columnNumber: 14
                    }, this))
            }, void 0, false, {
                fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
                lineNumber: 34,
                columnNumber: 10
            }, this)
        ]
    }, void 0, true, {
        fileName: "[project]/app/(public)/blog/[id]/SameArticles.tsx",
        lineNumber: 21,
        columnNumber: 5
    }, this);
}
}),
"[project]/app/(public)/blog/[id]/page.tsx [app-rsc] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "default",
    ()=>BlogPage,
    "generateStaticParams",
    ()=>generateStaticParams
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/server/route-modules/app-page/vendored/rsc/react-jsx-dev-runtime.js [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$utils$2f$index$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/src/utils/index.ts [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$api$2f$navigation$2e$react$2d$server$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__$3c$locals$3e$__ = __turbopack_context__.i("[project]/node_modules/next/dist/api/navigation.react-server.js [app-rsc] (ecmascript) <locals>");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$client$2f$components$2f$navigation$2e$react$2d$server$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/client/components/navigation.react-server.js [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$app$2f28$public$292f$blog$2f5b$id$5d2f$SameArticles$2e$tsx__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/app/(public)/blog/[id]/SameArticles.tsx [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/server/route-modules/app-page/vendored/rsc/react.js [app-rsc] (ecmascript)");
;
;
;
;
;
async function generateStaticParams() {
    return [
        {
            id: "1"
        },
        {
            id: "2"
        },
        {
            id: "3"
        }
    ];
}
async function BlogPage({ params }) {
    const { id } = await params;
    const article = __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$utils$2f$index$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["articles"].find((item)=>item.id === Number(id));
    if (article === undefined) {
        (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$client$2f$components$2f$navigation$2e$react$2d$server$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["notFound"])();
    }
    await (0, __TURBOPACK__imported__module__$5b$project$5d2f$src$2f$utils$2f$index$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["delay"])(1000);
    return /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
        children: [
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("article", {
                className: "max-w-2xl mx-auto",
                children: [
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("header", {
                        className: "mb-8 text-center",
                        children: [
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("span", {
                                className: "text-xs font-semibold text-blue-600 bg-blue-50 px-2.5 py-1 rounded-md mb-4 inline-block",
                                children: [
                                    "Запись #",
                                    id
                                ]
                            }, void 0, true, {
                                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                lineNumber: 25,
                                columnNumber: 11
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("h1", {
                                className: "text-3xl font-extrabold text-slate-900 tracking-tight sm:text-4xl",
                                children: article.title
                            }, void 0, false, {
                                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                lineNumber: 28,
                                columnNumber: 11
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                        lineNumber: 24,
                        columnNumber: 9
                    }, this),
                    /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "text-lg leading-relaxed text-slate-700 bg-slate-50 p-6 rounded-xl border border-slate-100",
                        children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                            children: article.content
                        }, void 0, false, {
                            fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                            lineNumber: 34,
                            columnNumber: 11
                        }, this)
                    }, void 0, false, {
                        fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                        lineNumber: 33,
                        columnNumber: 9
                    }, this)
                ]
            }, void 0, true, {
                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                lineNumber: 23,
                columnNumber: 7
            }, this),
            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                className: "mt-8",
                children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["Suspense"], {
                    fallback: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                        className: "mt-12 pt-8 border-t border-slate-200",
                        children: [
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                className: "animate-pulse flex space-x-4",
                                children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                    className: "flex-1 space-y-4 py-1",
                                    children: [
                                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                            className: "h-4 bg-slate-200 rounded w-1/4"
                                        }, void 0, false, {
                                            fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                            lineNumber: 42,
                                            columnNumber: 17
                                        }, this),
                                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                            className: "h-4 bg-slate-200 rounded w-1/2"
                                        }, void 0, false, {
                                            fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                            lineNumber: 43,
                                            columnNumber: 17
                                        }, this),
                                        /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                            className: "space-y-2",
                                            children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
                                                className: "h-20 bg-slate-200 rounded"
                                            }, void 0, false, {
                                                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                                lineNumber: 45,
                                                columnNumber: 19
                                            }, this)
                                        }, void 0, false, {
                                            fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                            lineNumber: 44,
                                            columnNumber: 17
                                        }, this)
                                    ]
                                }, void 0, true, {
                                    fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                    lineNumber: 41,
                                    columnNumber: 15
                                }, this)
                            }, void 0, false, {
                                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                lineNumber: 40,
                                columnNumber: 13
                            }, this),
                            /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])("p", {
                                className: "text-slate-500 mt-4 text-sm",
                                children: "Ищем похожие по тегам..."
                            }, void 0, false, {
                                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                                lineNumber: 49,
                                columnNumber: 13
                            }, this)
                        ]
                    }, void 0, true, {
                        fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                        lineNumber: 39,
                        columnNumber: 11
                    }, this),
                    children: /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$server$2f$route$2d$modules$2f$app$2d$page$2f$vendored$2f$rsc$2f$react$2d$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["jsxDEV"])(__TURBOPACK__imported__module__$5b$project$5d2f$app$2f28$public$292f$blog$2f5b$id$5d2f$SameArticles$2e$tsx__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["default"], {
                        article: article
                    }, void 0, false, {
                        fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                        lineNumber: 52,
                        columnNumber: 11
                    }, this)
                }, void 0, false, {
                    fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                    lineNumber: 38,
                    columnNumber: 9
                }, this)
            }, void 0, false, {
                fileName: "[project]/app/(public)/blog/[id]/page.tsx",
                lineNumber: 37,
                columnNumber: 7
            }, this)
        ]
    }, void 0, true, {
        fileName: "[project]/app/(public)/blog/[id]/page.tsx",
        lineNumber: 22,
        columnNumber: 5
    }, this);
}
}),
"[project]/app/(public)/blog/[id]/page.tsx [app-rsc] (ecmascript, Next.js Server Component)", ((__turbopack_context__) => {

__turbopack_context__.n(__turbopack_context__.i("[project]/app/(public)/blog/[id]/page.tsx [app-rsc] (ecmascript)"));
}),
];

//# sourceMappingURL=%5Broot-of-the-server%5D__0lb9h47._.js.map