module.exports = [
"[project]/app/apiTest/action.ts [app-rsc] (ecmascript)", ((__turbopack_context__) => {
"use strict";

/* __next_internal_action_entry_do_not_use__ [{"00b26fbf3a68ca0f5beca08eda3cb405e4e517c81f":{"name":"GetNotes"},"4008cb7cf693e969e5f1571c22b95b11fcbbaf006e":{"name":"PutNotes"},"407b639924480e879eb6a65426a4fd9ac802e32bd6":{"name":"PostNotes"},"40c78acc9f07466c54d1cdb5096d6dcf53cdfdb614":{"name":"DeleteNotes"}},"app/apiTest/action.ts",""] */ __turbopack_context__.s([
    "DeleteNotes",
    ()=>DeleteNotes,
    "GetNotes",
    ()=>GetNotes,
    "PostNotes",
    ()=>PostNotes,
    "PutNotes",
    ()=>PutNotes
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$server$2d$reference$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/build/webpack/loaders/next-flight-loader/server-reference.js [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$cache$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/cache.js [app-rsc] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$action$2d$validate$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/build/webpack/loaders/next-flight-loader/action-validate.js [app-rsc] (ecmascript)");
;
;
async function GetNotes() {
    const jsonNotes = await fetch(`${process.env.BASE_URL}/apiTest/api`);
    return await jsonNotes.json();
}
async function PostNotes(text) {
    await fetch(`${process.env.BASE_URL}/apiTest/api`, {
        method: "POST",
        body: JSON.stringify({
            text
        })
    });
    (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$cache$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["revalidatePath"])("/apiTest");
}
async function PutNotes(note) {
    console.log(`PutNotes: ${note.text}:${note.id}`);
    await fetch(`${process.env.BASE_URL}/apiTest/api/${note.id}`, {
        method: "PUT",
        body: JSON.stringify(note.text)
    });
    (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$cache$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["revalidatePath"])("/apiTest");
}
async function DeleteNotes(id) {
    await fetch(`${process.env.BASE_URL}/apiTest/api/${id}`, {
        method: "DELETE"
    });
    (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$cache$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["revalidatePath"])("/apiTest");
}
;
(0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$action$2d$validate$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["ensureServerEntryExports"])([
    GetNotes,
    PostNotes,
    PutNotes,
    DeleteNotes
]);
(0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$server$2d$reference$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["registerServerReference"])(GetNotes, "00b26fbf3a68ca0f5beca08eda3cb405e4e517c81f", null);
(0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$server$2d$reference$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["registerServerReference"])(PostNotes, "407b639924480e879eb6a65426a4fd9ac802e32bd6", null);
(0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$server$2d$reference$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["registerServerReference"])(PutNotes, "4008cb7cf693e969e5f1571c22b95b11fcbbaf006e", null);
(0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$webpack$2f$loaders$2f$next$2d$flight$2d$loader$2f$server$2d$reference$2e$js__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["registerServerReference"])(DeleteNotes, "40c78acc9f07466c54d1cdb5096d6dcf53cdfdb614", null);
}),
"[project]/.next-internal/server/app/apiTest/page/actions.js { ACTIONS_MODULE0 => \"[project]/app/apiTest/action.ts [app-rsc] (ecmascript)\" } [app-rsc] (server actions loader, ecmascript) <locals>", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([]);
var __TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/app/apiTest/action.ts [app-rsc] (ecmascript)");
;
;
;
;
;
;
;
}),
"[project]/.next-internal/server/app/apiTest/page/actions.js { ACTIONS_MODULE0 => \"[project]/app/apiTest/action.ts [app-rsc] (ecmascript)\" } [app-rsc] (server actions loader, ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "00b26fbf3a68ca0f5beca08eda3cb405e4e517c81f",
    ()=>__TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["GetNotes"],
    "4008cb7cf693e969e5f1571c22b95b11fcbbaf006e",
    ()=>__TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["PutNotes"],
    "407b639924480e879eb6a65426a4fd9ac802e32bd6",
    ()=>__TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["PostNotes"],
    "40c78acc9f07466c54d1cdb5096d6dcf53cdfdb614",
    ()=>__TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__["DeleteNotes"]
]);
var __TURBOPACK__imported__module__$5b$project$5d2f2e$next$2d$internal$2f$server$2f$app$2f$apiTest$2f$page$2f$actions$2e$js__$7b$__ACTIONS_MODULE0__$3d3e$__$225b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$2922$__$7d$__$5b$app$2d$rsc$5d$__$28$server__actions__loader$2c$__ecmascript$29$__$3c$locals$3e$__ = __turbopack_context__.i('[project]/.next-internal/server/app/apiTest/page/actions.js { ACTIONS_MODULE0 => "[project]/app/apiTest/action.ts [app-rsc] (ecmascript)" } [app-rsc] (server actions loader, ecmascript) <locals>');
var __TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$rsc$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/app/apiTest/action.ts [app-rsc] (ecmascript)");
}),
];

//# sourceMappingURL=_1oxa5ar._.js.map