import AppRouter from "../router";

type Props = {
    children?: React.ReactNode;
};

export default function AppProviders({ children }: Props) {
    return (
        <>
            {children ?? <AppRouter />}
        </>
    );
}