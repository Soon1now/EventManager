import { BrowserRouter } from "react-router-dom";
import AppRoutes from "./routers";

export default function AppRouter() {
    return (
        <BrowserRouter>
            <AppRoutes />
        </BrowserRouter>
    );
}