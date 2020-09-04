import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export default function itemGroupsReducer(
  state = initialState.itemGroups,
  action
) {
  switch (action.type) {
    case types.LOAD_ITEM_GROUPS_SUCCESS:
      return action.itemGroups;
    case types.CLEAR_ITEM_GROUPS:
      return [];
    default:
      return state;
  }
}
