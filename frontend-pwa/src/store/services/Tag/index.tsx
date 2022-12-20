import { createApi } from "@reduxjs/toolkit/query/react";
import { ITag } from "../../types/delete-Tag";
import { baseQueryWithReauth } from "../FetchBaseQuery";
import {
    ICategoryData,
    IRequestPeopleAddressByTagV2,
  IResultPeopleAddressByTag,
  ITagCategory,
} from "../../types/Category";
export const tagApi = createApi({
  reducerPath: "tagApi",
  tagTypes: ["Tag", "TagCategory", "PeopleAddressByTag"],
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getTagCategory: builder.query<ITagCategory[], void>({
      query: () => `Category/GetCategories`,
      providesTags: (result) => {
        return result
          ? [
              ...result.map(({ id }) => ({ type: "TagCategory" as const, id })),
              { type: "TagCategory", id: "LIST" },
            ]
          : [{ type: "TagCategory", id: "LIST" }];
      },
    }),
    getPeopleAddressByTag: builder.query<IResultPeopleAddressByTag[],IRequestPeopleAddressByTagV2>({
      query: (request) => {
        return {
          url: "Tag/GetTagWithAddressesWithinMapBound",
          method: "POST",
          body: request,
        };
      },
      providesTags: [{ type: "PeopleAddressByTag", id: "List" }],
    }),
    getCategoriesWithTags: builder.query<ICategoryData[],void>({
        query: () => `Tag/GetTagsWithAddressCountOfPerson`,
        providesTags: [{ type: "TagCategory", id: "List" }],
      }),
    createTagCategory: builder.mutation<string, Partial<ITagCategory>>({
      query: (category) => ({
        url: "Category/CreateTagCategory",
        method: "POST",
        body: category,
      }),
      invalidatesTags: (result, error, { id }) => [
        { type: "TagCategory", id: "LIST" },
      ],
    }),
    updateTagCategory: builder.mutation<number, Partial<ITagCategory>>({
      query: (category) => ({
        url: "Category/UpdateTagCategory",
        method: "PUT",
        body: category,
      }),
      invalidatesTags: (result, error, { id }) => [
        { type: "TagCategory", id: "LIST" },
      ],
    }),
    deleteTagCategory: builder.mutation<number, string>({
      query: (id) => ({
        url: `Category/Delete/${id}`,
        method: "DELETE",
        body: { id: id },
      }),
      invalidatesTags: (result, error, id) => [
        { type: "TagCategory", id: "LIST" },
      ],
    }),
    findTagByCategoryId: builder.query<ITag[], string>({
      query: (id) => `Tag/FindTagByCategoryId/${id}`,
      providesTags: (result) => {
        return result
          ? [
              ...result.map(({ id }) => ({ type: "Tag" as const, id })),
              { type: "Tag", id: "LIST" },
              { type: "TagCategory", id: "LIST" },
            ]
          : [
              { type: "Tag", id: "LIST" },
              { type: "TagCategory", id: "LIST" },
            ];
      },
    }),
    getTags: builder.query<ITag[], void>({
      query: () => `Tag/GetTags`,
      providesTags: (result) => {
        return result
          ? [
              ...result.map(({ id }) => ({ type: "Tag" as const, id })),
              { type: "Tag", id: "LIST" },
            ]
          : [{ type: "Tag", id: "LIST" }];
      },
    }),
    createTag: builder.mutation<string, ITag>({
      query: (tag) => ({
        url: "Tag/CreateTag",
        method: "POST",
        body: tag,
      }),
      invalidatesTags: (result, error, { id }) => [
        { type: "Tag", id: "LIST" },
        { type: "TagCategory", id: "LIST" },
      ],
    }),
    updateTag: builder.mutation<number, ITag>({
      query: (tag) => ({
        url: "Tag/UpdateTag",
        method: "PUT",
        body: tag,
      }),
      invalidatesTags: (result, error, { id }) => [
        { type: "Tag", id: id },
        { type: "TagCategory", id: "LIST" },
      ],
    }),
    deleteTag: builder.mutation<number, string>({
      query: (id) => ({
        url: `Tag/DeleteTag/${id}`,
        method: "DELETE",
        body: { id: id },
      }),
      invalidatesTags: (result, error, id) => [
        { type: "Tag", id: id },
        { type: "TagCategory", id: "LIST" },
      ],
    }),
  }),
});
export const {
  useGetTagCategoryQuery,
  useCreateTagCategoryMutation,
  useUpdateTagCategoryMutation,
  useDeleteTagCategoryMutation,
  useGetTagsQuery,
  useFindTagByCategoryIdQuery,
  useCreateTagMutation,
  useUpdateTagMutation,
  useDeleteTagMutation,
  useGetCategoriesWithTagsQuery,
  useGetPeopleAddressByTagQuery
} = tagApi;
