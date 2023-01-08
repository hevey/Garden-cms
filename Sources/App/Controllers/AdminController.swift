import Vapor

struct AdminController: RouteCollection {
    func boot(routes: Vapor.RoutesBuilder) throws {
        let admin = routes.grouped("garden")
        
        admin.get(use: index)
    }
    
    func index(req: Request) async throws -> View {
        return try await req.view.render("admin/index", ["title": "Hello Garden!"])
    }
}
