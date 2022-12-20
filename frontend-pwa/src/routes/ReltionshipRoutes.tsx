import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
import RelationshipTypeList from '../views/RelationshipType/RelationshipTypeList';
const ReltaionshipList = Loadable(lazy(() => import('../views/Relationship/RelationshipList')));

const ReltaionRoutes = {
    path: '/relationship',
    element: <MainLayout/>,
    children: [
        {
            path: '/relationship/:personid/:personName',
            element: <ReltaionshipList/>
        },
        {
            path: '/relationship/relationshipType',
            element: <RelationshipTypeList/>
        },
         ]
 };

export default ReltaionRoutes;
