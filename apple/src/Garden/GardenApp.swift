//
//  GardenApp.swift
//  Garden
//
//  Created by Glenn Hevey on 6/6/2023.
//

import SwiftUI
import SwiftData

@main
struct GardenApp: App {

    var body: some Scene {
        WindowGroup {
            ContentView()
        }
        .modelContainer(for: Item.self)
    }
}
