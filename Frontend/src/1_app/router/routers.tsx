import { Route, Routes, Navigate } from "react-router-dom";
import HRLayout from "../layouts/HRLayout";
import EmployeeLayout from "../layouts/EmloyeeLayout";
import  ColorPalette  from "../../colorsPalette";

function Placeholder({ title }: { title: string }) {
    return <div>{title}</div>;
}

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/login" element={<Placeholder title="Авторизация" />} />

            <Route path="/hr" element={<HRLayout />}>
                <Route index element={<Placeholder title="HR Dashboard" />} />
                <Route path="events" element={<Placeholder title="HR Events" />} />
            </Route>

            <Route path="/employee" element={<EmployeeLayout />}>
                <Route index element={<Placeholder title="Employee Dashboard" />} />
                <Route path="events" element={<Placeholder title="Events" />} />
            </Route>

            <Route path="/" element={<Navigate to="/login" replace />} />
            <Route path="/colors" element={<ColorPalette />} />
            <Route path="*" element={<Placeholder title="404 — Page Not Found" />} />
        </Routes>
    );
}