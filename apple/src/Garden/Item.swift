//
//  Item.swift
//  Garden
//
//  Created by Glenn Hevey on 6/6/2023.
//

import Foundation
import SwiftData

@Model
final class Item {
    var timestamp: Date
    
    init(timestamp: Date) {
        self.timestamp = timestamp
    }
}
