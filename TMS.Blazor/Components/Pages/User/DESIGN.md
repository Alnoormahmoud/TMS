# Design System Strategy: The Architectural Trust

This document outlines the visual and structural language of the design system. It is designed to move beyond the "generic fintech" aesthetic, replacing standard grid-based layouts with a sophisticated, editorial approach to financial management.

## 1. Overview & Creative North Star: "The Digital Private Bank"
The Creative North Star for this system is **The Digital Private Bank**. We are moving away from the "busy" nature of traditional banking apps toward a high-end, curated experience. The goal is to make the user feel like they are looking at a bespoke financial statement, not a database.

To achieve this, we break the "template" look through:
*   **Intentional Asymmetry:** Using unbalanced white space to guide the eye toward key actions.
*   **Tonal Depth:** Replacing harsh lines with a hierarchy of "paper" surfaces.
*   **Typographic Authority:** Using high-contrast scales between oversized Display text and micro-labels to create an editorial rhythm.

---

## 2. Colors: Tonal Architecture
The palette is rooted in a professional blue-and-white spectrum, but its application must be nuanced to avoid looking like a default framework.

### The "No-Line" Rule
**Explicit Instruction:** Designers are prohibited from using 1px solid borders to section content. Boundaries must be defined solely through background color shifts. Use `surface-container-low` for sections sitting on a `surface` background. If you need to separate content, use white space or a change in the `surface` token.

### Surface Hierarchy & Nesting
Treat the UI as a series of physical layers. Use the `surface-container` tiers to create depth:
*   **Foundation:** `surface` (#f7f9fb)
*   **Sectioning:** `surface-container-low` (#f2f4f6)
*   **Primary Containers:** `surface-container-lowest` (#ffffff) for high-importance cards.
*   **Interactive Overlays:** `surface-container-highest` (#e0e3e5) for subtle hover states or depressed elements.

### The "Glass & Gradient" Rule
To inject "soul" into the interface:
*   **Floating Elements:** Use Glassmorphism for navigation bars or floating action buttons. Apply a semi-transparent `surface` color with a 20px backdrop-blur.
*   **Signature Textures:** For main CTAs and Hero sections, use a subtle linear gradient from `primary` (#003ec7) to `primary_container` (#0052ff) at a 135-degree angle. This adds a "lithographic" quality that flat colors lack.

---

## 3. Typography: The Editorial Scale
We use two typefaces: **Manrope** for authoritative headlines and **Inter** for functional data.

*   **Display & Headlines (Manrope):** These should feel cinematic. Use `display-lg` for account balances to give them a sense of weight and importance. The geometric nature of Manrope conveys stability and modernity.
*   **Body & Labels (Inter):** Inter is used for high-legibility tasks. Use `label-md` for transaction metadata—the neutral, tall x-height ensures clarity even at small scales.
*   **Visual Hierarchy:** Always pair a `headline-sm` with a `body-sm` in `on_surface_variant` (#434656) to create a clear "Title and Caption" relationship without needing a divider.

---

## 4. Elevation & Depth: Tonal Layering
We do not use shadows to create "pop"; we use them to create "atmosphere."

*   **The Layering Principle:** Stack your surfaces. A `surface-container-lowest` card (Pure White) should sit on a `surface-container-low` section (Soft Grey). The 2-point difference in hex value provides enough contrast for the human eye without creating visual noise.
*   **Ambient Shadows:** For elevated components (Modals/Floating Cards), use a multi-layered shadow:
    *   *Offset: 0px 10px | Blur: 30px | Color: `on_surface` at 4% opacity.*
    *   This mimics natural light and keeps the interface feeling "airy."
*   **The "Ghost Border" Fallback:** If a border is required for accessibility (e.g., in high-contrast modes), use the `outline_variant` (#c3c5d9) at **15% opacity**. Never use a 100% opaque border.

---

## 5. Components

### Cards & Lists
*   **Strict Rule:** No divider lines.
*   Separate transaction items using `16px` of vertical white space.
*   Use a `surface-container-low` background on hover to indicate interactivity.

### Buttons
*   **Primary:** Gradient fill (Primary to Primary Container) with `xl` (0.75rem) roundedness.
*   **Secondary:** `surface-container-high` background with `on_surface` text. No border.
*   **Tertiary:** Transparent background with `primary` text. Use for low-priority actions like "View All."

### Input Fields
*   Use `surface-container-lowest` as the fill. 
*   Instead of a full border, use a 2px bottom-stroke of `outline_variant` that transforms into `primary` on focus.
*   Floating labels should use `label-sm` for a precision-engineered look.

### Banking Specific: The "Wealth Card"
A signature component for this system. A large-format container using `primary` as the background, featuring a subtle noise texture and a `display-md` balance. This is the only element allowed to break the minimalist white aesthetic.

---

## 6. Do’s and Don’ts

### Do
*   **DO** use exaggerated white space (32px, 48px, 64px) to separate major functional groups.
*   **DO** use `surface_tint` (#004ced) sparingly for micro-interactions like active toggle states or progress bars.
*   **DO** ensure that numerical data (amounts) uses tabular lining (monospaced numbers) within the Inter font for alignment.

### Don't
*   **DON'T** use black (#000000). Use `on_surface` (#191c1e) for all "black" text to maintain the sophisticated blue-tinted depth.
*   **DON'T** use "Drop Shadows" on text.
*   **DON'T** use standard 8px rounding for everything. Use the scale: `xl` (12px) for large containers and `sm` (2px) for status indicators to create a "varied" professional geometry.

---

## 7. Roundedness Scale
*   **Small (`sm`):** 0.125rem (Status indicators, tiny tags)
*   **Medium (`md`):** 0.375rem (Input fields)
*   **Large (`lg`):** 0.5rem (Standard buttons)
*   **Extra Large (`xl`):** 0.75rem (Main content cards, "Wealth Cards")
*   **Full (`full`):** 9999px (Pills, Search bars)