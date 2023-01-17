/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        '../{Pages,Shared}/**/*.{cshtml,razor,cs,css}'
    ],
    theme: {
        extend: {},
    },
    plugins: [
    ],
    safelist: [
        {
            pattern: /.*/,
            // variants: [
            //     "first",
            //     "last",
            //     "odd",
            //     "even",
            //     "visited",
            //     "checked",
            //     "empty",
            //     "read-only",
            //     "group-hover",
            //     "group-focus",
            //     "focus-within",
            //     "hover",
            //     "focus",
            //     "focus-visible",
            //     "active",
            //     "disabled",
            // ],
        },
    ],
}