import * as types from "./actionTypes";
import * as itemGroupRepository from "../../repositories/itemGroupRepository";

export function loadItemGroups(queryParameters) {
  return async function (dispatch) {
    try {
      const response = await itemGroupRepository.getItemGroups(queryParameters);
      dispatch({
        type: types.SET_ITEM_GROUPS_PAGINATION,
        itemGroupsPagination: response.itemGroupsPagination,
      });
      return dispatch({
        type: types.LOAD_ITEM_GROUPS_SUCCESS,
        itemGroups: response.itemGroups,
      });
    } catch (error) {
      throw error;
    }
  };
}

export function clearItemGroups() {
  return {
    type: types.CLEAR_ITEM_GROUPS,
  };
}

export function loadSelectedItemGroup(id) {
  return async function (dispatch) {
    try {
      const selectedItemGroup = await itemGroupRepository.get(id);
      return dispatch({
        type: types.LOAD_SELECTED_ITEM_GROUP_SUCCESS,
        selectedItemGroup,
      });
    } catch (error) {
      throw error;
    }
  };
}

export function setItemGroupsPagination(itemGroupsPagination) {
  return {
    type: types.SET_ITEM_GROUPS_PAGINATION,
    itemGroupsPagination: itemGroupsPagination,
  };
}

export function deleteItemGroup(id) {
  return async function (dispatch) {
    try {
      await itemGroupRepository.remove(id);
      return dispatch({
        type: types.DELETE_ITEM_GROUP_SUCCESS,
      });
    } catch (error) {
      throw error;
    }
  };
}

export function saveItemGroup(itemGroup) {
  return async function (dispatch) {
    try {
      const response = await itemGroupRepository.save(itemGroup);
      return dispatch({
        type: types.NEW_ITEM_GROUP_SUCCESS,
        newItemGroup: response,
      });
    } catch (error) {
      throw error;
    }
  };
}

export function setNewItemGroup(itemGroup) {
  return {
    type: types.NEW_ITEM_GROUP_SUCCESS,
    newItemGroup: itemGroup,
  };
}
