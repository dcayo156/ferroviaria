import * as React from 'react';
import { Box,  FormControl,  InputLabel,  Select, MenuItem, OutlinedInput, SelectChangeEvent, Container } from '@mui/material';
import { IInspectionTrainOptions } from '../../../store/types/InspectionTrain';
import { useGetListCategoryQuery } from '../../../store/services/Category';
interface FormSelectOptionProps {
    InspectionTrain: IInspectionTrainOptions
    setInspectionTrain: (value: React.SetStateAction<IInspectionTrainOptions>) => void
}

const FormSelectOption: React.FunctionComponent<FormSelectOptionProps> = ({ InspectionTrain, setInspectionTrain }) => {
    const { data: categoryData, error, isLoading } = useGetListCategoryQuery();
    
    const ITEM_HEIGHT = 38;
    const MenuProps = {
        PaperProps: {
            style: {
            maxHeight: ITEM_HEIGHT * 4.5 ,
            width: 250,
            },
        },
    };
    
    const handleChangeChildrenSelect = (event: SelectChangeEvent) => {
        setInspectionTrain({ ...InspectionTrain, subCategoryId: event.target.value })
    }
    const handleChangeParentSelect= (event: SelectChangeEvent)=>{
        setInspectionTrain({ ...InspectionTrain, categoryId: event.target.value,subCategoryId:"" })
    }

  return <Container sx={{pb:3}}
  component="main" maxWidth="xs" >
  <Box
      sx={{
          marginTop: 2,
          display: "flex",
          flexDirection: "row",
          alignItems: "center",
      }}
  >            
      <Box
          component="form"
          noValidate
          sx={{flex: 1 }}
      >
          <FormControl sx={{ width: "100%",pb:2 }}>
              <InputLabel id="select-category-label-1">Categoría Padre</InputLabel>
                  <Select
                  id="select-category-1"
                  value={InspectionTrain.categoryId || '' }
                  onChange={handleChangeParentSelect}
                  input={<OutlinedInput label="Categoria" />}
                  MenuProps={MenuProps}
                  name={"parentCategoryId"}
                  >
                      <MenuItem
                      key="undefiend"
                      value={undefined}
                      >
                      Seleccione una Categoría
                      </MenuItem>
                      {
                          categoryData?.filter(cat=>cat.parentCategoryId==undefined).map(cat=>{
                              return <MenuItem
                              key={cat.id}
                              value={cat.id}
                              >
                              {cat.name}
                              </MenuItem>
                          })
                      }
                  </Select>
          </FormControl>
          </Box>
          <Box
          component="form"
          noValidate
          sx={{ flex: 1 }}
      >
          <FormControl sx={{ width: "100%",pb:2 }}>
              <InputLabel id="select-category-label-2">Categoría hijo</InputLabel>
                  <Select
                  id="select-category-2"
                  value={InspectionTrain.subCategoryId || '' }
                  onChange={handleChangeChildrenSelect}
                  input={<OutlinedInput label="SubCategoria" />}
                  MenuProps={MenuProps}
                  name={"subCategoryId"}
                  >
                      <MenuItem
                      key="undefiend"
                      value={undefined}
                      >
                      Seleccione una Subcategoría
                      </MenuItem>
                      {
                          categoryData?.filter(cat=>cat.parentCategoryId==InspectionTrain.categoryId).map(cat=>{
                              return <MenuItem
                              key={cat.id}
                              value={cat.id}
                              >
                              {cat.name}
                              </MenuItem>
                          })
                      }
                  </Select>
          </FormControl>
          
      </Box>
  </Box>          
</Container>
}
export default FormSelectOption;