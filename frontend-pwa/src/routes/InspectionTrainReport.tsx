import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const InspectionTrainsForYear = Loadable(lazy(() => import('../views/Report/InspectionTrainsForYear')));
const InspectionTrainReportRoute = {
    path: '/Report',
    element: <MainLayout/>,
    children: [
        {
            path: '/Report',
            element: <InspectionTrainsForYear/>
        }
    ]
 };

export default InspectionTrainReportRoute;
