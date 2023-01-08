//
//  Item.swift
//  
//
//  Created by Glenn Hevey on 8/1/2023.
//

import Fluent

final class Item: Model {
    static let schema = "item_type"
    
    @ID(key: .id)
    var id: UUID?
    
    @Field(key: "name")
    var name: String
    
    init() { }
    
    init(id: UUID? = nil, name: String) {
        self.id = id
        self.name = name
    }
}
