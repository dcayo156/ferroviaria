import { Grid, Paper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useGetCategoriesWithTagsQuery, useGetPeopleAddressByTagQuery } from "../../store/services/Tag";
import { ICategoryData, IRequestPeopleAddressByTagV2, IResultPeopleAddressByTag, ITagItem, Category } from "../../store/types/Category";
import MyMap from "./SearchMap";
import AccordionList from "./CheckBoxList/Index";
import { StyledTag } from "./CheckBoxList/StyledTag";

type Props = {};
const fromLucene=((process.env.REACT_APP_GET_ADDRESS_IN_MAP_BOUND_FROM_LUCENE as string)==="true"?true:false);
export default function SearchPage({}: Props) {
  const { data:categories, error, isLoading } = useGetCategoriesWithTagsQuery();
  const [requestPeopleByTag, setRequestPeopleByTag] = useState<IRequestPeopleAddressByTagV2>(
    {
      to:{
        latitud:0,
        longitud:0
      },
      from:{
        latitud:0,
        longitud:0
      },
      fromLucene:fromLucene
    }
  );
  const { data:requestResult, } = useGetPeopleAddressByTagQuery(requestPeopleByTag);

  const [zoom, setZoom] = useState<number>(15);
  const [count, setCount] = useState(0)
  const [googleMap, setGoogleMap] = useState<any>(null);
  const [people, setPeople] = useState<any[]>([]);
  const [center, setCenter] = useState<
    google.maps.LatLng | google.maps.LatLngLiteral | undefined
  >({
    lat: -34.63714351232913,
    lng: -58.7029105423287,
  });
  const [selectedPerson, setSelectedPerson] = useState<IResultPeopleAddressByTag|null>(null);
  const [isLoaded, setIsLoaded] = useState<boolean>(false);
  const [categoryData, setCategoryData] = useState<ICategoryData[]>([]);

  const BuildAndSendRequest = (map: any) => {
    if (map && map.getBounds()) {
      let ne = map!.getBounds().getNorthEast();
      let sw = map!.getBounds().getSouthWest();
      let arrayCategory:Category[]=categoryData.map((category)=>{
        return {
          id: category.id,
          tagIds: category.tags.filter(cat=>cat.selected).map((tag)=>tag.id)
        }
      }).filter((category)=>category.tagIds.length>0);
      const request:IRequestPeopleAddressByTagV2 = {
        from:{
          latitud:ne.lat(),
          longitud:sw.lng()
        },
        to:{
          latitud:sw.lat(),
          longitud:ne.lng()
        },
        fromLucene:fromLucene,
        categories:arrayCategory
      };
      setRequestPeopleByTag(request);
    } else {
      const request:IRequestPeopleAddressByTagV2 = {
        from:{
          latitud:-34.629705360129776,
          longitud:-58.71186668733232
        },
        to:{
          latitud:-34.64735975191487,
          longitud:-58.69040901521318
        },
        fromLucene:fromLucene
      };
      setRequestPeopleByTag(request);
    }
  };

  const onDrag = () => {
    if (!googleMap) return;
    BuildAndSendRequest(googleMap);
  };

  useEffect(() => {
    if (requestResult) {
      setPeople(requestResult);
    }
  }, [requestResult]);

  useEffect(() => {
    if (categoryData) {
      if (!googleMap) return;
      BuildAndSendRequest(googleMap);
    }
  }, [categoryData]);

  useEffect(() => {
    if (categories) {
      setCategoryData(categories!.map((category) => {
        return {
          name: category.name,
          id: category.id,
          tags: category.tags.map((tag) => {
            return {
              name: tag.name,
              id: tag.id,
              numberOfPeople: tag.numberOfPeople,
              selected: false,
            };
          }),
        };
      }));
    }
  }, [categories]);

  const setCategory = (tagId: string, tagCategoryId: string) => {
    let arrayCategory = [...categoryData];
    let selectedCategory = arrayCategory.find((x) => x.id === tagCategoryId);
    const selectedTag = selectedCategory?.tags.find((x) => x.id === tagId);
    if (selectedTag) {
      selectedTag.selected = !selectedTag?.selected;
    }

    let indexCategory = arrayCategory.findIndex((x) => x.id === tagCategoryId);
    const indexTag = selectedCategory?.tags.findIndex((x) => x.id === tagId);

    selectedCategory?.tags.splice(
      indexTag as number,
      1,
      selectedTag as ITagItem
    );
    arrayCategory.splice(
      indexCategory as number,
      1,
      selectedCategory as ICategoryData
    );

    setCategoryData(arrayCategory);
 };

  const onDeleteTag = (id: string) => {

    let category = categoryData.find(
      (f) => f.tags.find((x) => x.id === id) !== undefined
    );
    setCategory(id, category?.id as string);
  };
  const getIsLoaded = (load: boolean,) => {
    setCount(count + 1)
    setIsLoaded(load)
  }
  const getMap = (m: any) => {
    setGoogleMap(m)
    BuildAndSendRequest(m);
  }
  const onSetSelectedPerson=(person:IResultPeopleAddressByTag|null) =>{
      setSelectedPerson(person);
  };
  return (
    <Grid container spacing={3}>
      <Grid item xs={12} sm={12} md={12} lg={3}>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Paper sx={{ p: 2, minHeight: "100px" }}>
              <Typography variant="h6">Filtros Seleccionados</Typography>
              <hr />
              <div style={{ display: "flex", flexWrap: "wrap" }}>
                {categoryData.map((category, index) => {
                  return category.tags.filter((tag=>tag.selected)).map((tag,index)=>{
                    return (
                      <StyledTag
                        label={tag.name}
                        key={index}
                        tabIndex={-1}
                        value={tag.id}
                        onDelete={(event: any) => {}}
                        onDeleteTag={onDeleteTag}
                        data-tag-index={index}
                      />
                    );
                  })
                  
                })}
              </div>
            </Paper>
          </Grid>
          <Grid item xs={12}>
            <Paper sx={{ p: 2, minHeight: "100px" }}>
              {categoryData.map((category) => {
                return (
                  <AccordionList
                    key={category.id}
                    category={category}
                    setCategory={setCategory}
                  />
                );
              })}
            </Paper>
          </Grid>
        </Grid>
      </Grid>
      <Grid item xs={12} sm={12} md={12} lg={9}>
      <MyMap
            zoom={zoom}
            people={people}
            selectedPerson={selectedPerson}
            onDrag={onDrag}
            centerTo={center}
            isLoaded={isLoaded}
            getIsLoaded={getIsLoaded}
            googleMap={googleMap}
            getMap={getMap}
            onSetSelectedPerson={onSetSelectedPerson}
          />
      </Grid>
    </Grid>
  );
}