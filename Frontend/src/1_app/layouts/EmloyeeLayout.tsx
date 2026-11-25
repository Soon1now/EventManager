import { Outlet } from "react-router-dom";
import Sidebar from "../../3_widgets/SideBar";

export default function EmployeeLayout() {
    return (
        <div className="app-layout">
            <Sidebar role="Employee" />
            <div className="app-content">
                <Header />
                <main>
                    <Outlet />
                </main>
            </div>
        </div>
    );
}