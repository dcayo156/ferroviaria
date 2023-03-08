import { Box } from "@mui/material";
import MainCard from "../../components/cards/MainCard";
import BarChart from "../InspectionTrain/Forms/BarChart";

const ListInspectionTrainsForYear: React.FunctionComponent = () => {  

    return (
      <MainCard
            title="Inspección integral de trenes" secondary={undefined}                   
      >  
          
            <Box>
              <BarChart/>
            </Box>
          
      </MainCard>      
    );
  };
  
  export default ListInspectionTrainsForYear;