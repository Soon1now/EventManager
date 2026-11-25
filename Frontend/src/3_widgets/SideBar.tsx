
import { NavLink } from "react-router-dom";
import { CalendarDays, LayoutDashboard } from "lucide-react";

type Props = {
    role: "HR" | "Employee";
};

const navigation = {
    HR: [
        { title: "Главная", path: "/hr", icon: LayoutDashboard },
        { title: "Мероприятия", path: "/hr/events", icon: CalendarDays },
    ],
    Employee: [
        { title: "Главная", path: "/employee", icon: LayoutDashboard },
        { title: "Мероприятия", path: "/employee/events", icon: CalendarDays },
    ],
};

export default function Sidebar({ role }: Props) {
    return (
        <header className="w-full h-18 bg-surface border-b border-border">
            <div className="max-w-7xl h-full mx-auto px-8 flex items-center justify-between">
                <div className="flex items-center gap-3">

                    <span className="text-lg font-semibold tracking-tight text-text">
                        Events Manager
                    </span>
                </div>

                <nav className="flex items-center gap-2">
                    {navigation[role].map(({ title, path}) => (
                        <NavLink
                            key={path}
                            to={path}
                            end
                            className={({ isActive }) =>
                                `flex items-center gap-2 px-4 py-2 rounded-2xl text-sm font-medium transition-colors ${
                                    isActive
                                        ? "bg-primary-light text-primary-active"
                                        : "text-text-secondary hover:bg-surface-secondary hover:text-text"
                                }`
                            }
                        >
                            
                            {title}
                        </NavLink>
                    ))}
                </nav>
            </div>
        </header>
    );
}
