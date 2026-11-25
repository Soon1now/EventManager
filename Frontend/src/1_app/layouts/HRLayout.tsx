import { Outlet } from "react-router-dom";
import Sidebar from "../../3_widgets/SideBar";

export default function HRLayout() {
    return (
        <div className="min-h-screen bg-background">
            <Sidebar role="HR" />

            <main className="max-w-7xl mx-auto px-8 py-8">
                <Outlet />
            </main>
        </div>
    );
}