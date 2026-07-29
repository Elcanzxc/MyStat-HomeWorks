(globalThis["TURBOPACK"] || (globalThis["TURBOPACK"] = [])).push([typeof document === "object" ? document.currentScript : undefined,
"[project]/app/apiTest/action.ts [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "GetNotes",
    ()=>GetNotes,
    "PostNotes",
    ()=>PostNotes
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$polyfills$2f$process$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = /*#__PURE__*/ __turbopack_context__.i("[project]/node_modules/next/dist/build/polyfills/process.js [app-client] (ecmascript)");
async function GetNotes() {
    const jsonNotes = await fetch(`${__TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$build$2f$polyfills$2f$process$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["default"].env.BASE_URL}/apiTest/api`);
    return await jsonNotes.json();
}
_c = GetNotes;
async function PostNotes(noteText) {
    console.log("PostNotes");
    await fetch(`/apiTest/api`, {
        method: 'POST',
        body: JSON.stringify(noteText)
    });
}
_c1 = PostNotes;
var _c, _c1;
__turbopack_context__.k.register(_c, "GetNotes");
__turbopack_context__.k.register(_c1, "PostNotes");
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
"[project]/app/apiTest/createNoteForm.tsx [app-client] (ecmascript)", ((__turbopack_context__) => {
"use strict";

__turbopack_context__.s([
    "default",
    ()=>CreateNoteForm
]);
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/jsx-dev-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/compiler-runtime.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/node_modules/next/dist/compiled/react/index.js [app-client] (ecmascript)");
var __TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__ = __turbopack_context__.i("[project]/app/apiTest/action.ts [app-client] (ecmascript)");
;
var _s = __turbopack_context__.k.signature();
'use client';
;
;
;
function CreateNoteForm() {
    _s();
    const $ = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$compiler$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["c"])(11);
    if ($[0] !== "c3c882c3c4530c21d3b3d72fe11c791b77b639aea0d9b29b4facc99cd80e9569") {
        for(let $i = 0; $i < 11; $i += 1){
            $[$i] = Symbol.for("react.memo_cache_sentinel");
        }
        $[0] = "c3c882c3c4530c21d3b3d72fe11c791b77b639aea0d9b29b4facc99cd80e9569";
    }
    const [text, setText] = (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$index$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["useState"])("");
    let t0;
    if ($[1] !== text) {
        t0 = ({
            "CreateNoteForm[add]": async ()=>{
                console.log(text);
                await (0, __TURBOPACK__imported__module__$5b$project$5d2f$app$2f$apiTest$2f$action$2e$ts__$5b$app$2d$client$5d$__$28$ecmascript$29$__["PostNotes"])(text);
            }
        })["CreateNoteForm[add]"];
        $[1] = text;
        $[2] = t0;
    } else {
        t0 = $[2];
    }
    const add = t0;
    let t1;
    if ($[3] === Symbol.for("react.memo_cache_sentinel")) {
        t1 = ({
            "CreateNoteForm[<input>.onChange]": (e)=>setText(e.target.value)
        })["CreateNoteForm[<input>.onChange]"];
        $[3] = t1;
    } else {
        t1 = $[3];
    }
    let t2;
    if ($[4] !== text) {
        t2 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("input", {
            value: text,
            onChange: t1,
            placeholder: "vvedi text"
        }, void 0, false, {
            fileName: "[project]/app/apiTest/createNoteForm.tsx",
            lineNumber: 40,
            columnNumber: 10
        }, this);
        $[4] = text;
        $[5] = t2;
    } else {
        t2 = $[5];
    }
    let t3;
    if ($[6] !== add) {
        t3 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("button", {
            onClick: add,
            children: "Otpravit"
        }, void 0, false, {
            fileName: "[project]/app/apiTest/createNoteForm.tsx",
            lineNumber: 48,
            columnNumber: 10
        }, this);
        $[6] = add;
        $[7] = t3;
    } else {
        t3 = $[7];
    }
    let t4;
    if ($[8] !== t2 || $[9] !== t3) {
        t4 = /*#__PURE__*/ (0, __TURBOPACK__imported__module__$5b$project$5d2f$node_modules$2f$next$2f$dist$2f$compiled$2f$react$2f$jsx$2d$dev$2d$runtime$2e$js__$5b$app$2d$client$5d$__$28$ecmascript$29$__["jsxDEV"])("div", {
            children: [
                t2,
                t3
            ]
        }, void 0, true, {
            fileName: "[project]/app/apiTest/createNoteForm.tsx",
            lineNumber: 56,
            columnNumber: 10
        }, this);
        $[8] = t2;
        $[9] = t3;
        $[10] = t4;
    } else {
        t4 = $[10];
    }
    return t4;
}
_s(CreateNoteForm, "3t0DFnMi16eB/7p7iIKtjG5r68g=");
_c = CreateNoteForm;
var _c;
__turbopack_context__.k.register(_c, "CreateNoteForm");
if (typeof globalThis.$RefreshHelpers$ === 'object' && globalThis.$RefreshHelpers !== null) {
    __turbopack_context__.k.registerExports(__turbopack_context__.m, globalThis.$RefreshHelpers$);
}
}),
]);

//# sourceMappingURL=app_apiTest_1zp5zmw._.js.map