import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export default function itemGroupsPaginationReducer(
  state = initialState.itemGroupsPagination,
  action
) {
  switch (action.type) {
    case types.SET_ITEM_GROUPS_PAGINATION:
      return action.itemGroupsPagination;
    default:
      return state;
  }
}
