import * as React from 'react';
import { Box,  FormControl,  InputLabel,  Select, MenuItem, OutlinedInput, SelectChangeEvent, Container } from '@mui/material';
import { IDocumentOptions } from '../../../store/types/Document';
import { useGetListCategoryQuery } from '../../../store/services/Category';
interface FormSelectOptionProps {
    document: IDocumentOptions
    setDocument: (value: React.SetStateAction<IDocumentOptions>) => void
}

const FormSelectOption: React.FunctionComponent<FormSelectOptionProps> = ({ document, setDocument }) => {
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
        setDocument({ ...document, subCategoryId: event.target.value })
    }
    const handleChangeParentSelect= (event: SelectChangeEvent)=>{
        setDocument({ ...document, categoryId: event.target.value,subCategoryId:"" })
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
              <InputLabel id="select-category-label-1">Categoria Padre</InputLabel>
                  <Select
                  id="select-category-1"
                  value={document.categoryId || '' }
                  onChange={handleChangeParentSelect}
                  input={<OutlinedInput label="Categoria" />}
                  MenuProps={MenuProps}
                  name={"parentCategoryId"}
                  >
                      <MenuItem
                      key="undefiend"
                      value={undefined}
                      >
                      Seleccione una Categoria
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
              <InputLabel id="select-category-label-2">Categoria hijo</InputLabel>
                  <Select
                  id="select-category-2"
                  value={document.subCategoryId || '' }
                  onChange={handleChangeChildrenSelect}
                  input={<OutlinedInput label="SubCategoria" />}
                  MenuProps={MenuProps}
                  name={"subCategoryId"}
                  >
                      <MenuItem
                      key="undefiend"
                      value={undefined}
                      >
                      Seleccione una Subcategoria
                      </MenuItem>
                      {
                          categoryData?.filter(cat=>cat.parentCategoryId==document.categoryId).map(cat=>{
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