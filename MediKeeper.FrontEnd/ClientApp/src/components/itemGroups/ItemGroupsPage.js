import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as itemGroupActions from "../../redux/actions/itemGroupActions";
import PropTypes from "prop-types";
import ItemGroupList from "./ItemGroupList";
import queryString from "query-string";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";

function ItemGroupsPage({
  itemGroups,
  loadItemGroups,
  location,
  history,
  itemGroupsPagination,
  clearItemGroups,
  deleteItemGroup,
}) {
  const [fetchedAlready, setFetchedAlready] = useState(false);
  const [markedForDeletion, setMarkedForDeletion] = useState([]);

  useEffect(() => {
    async function fetchData() {
      try {
        const values = queryString.parse(location.search);

        const queryParameters = {
          ...itemGroupsPagination,
          name: values.name,
          pageSize: values.pageSize ? values.pageSize : 25,
          pageNumber:
            !values.pageNumber || itemGroupsPagination.totalPages <= 0
              ? 1
              : values.pageNumber > itemGroupsPagination.totalPages
              ? itemGroupsPagination.totalPages
              : values.pageNumber,
        };
        await loadItemGroups(queryParameters);
      } catch (error) {
        alert("Failed to load itemGroups. " + error);
      }
    }

    if (itemGroups.length <= 0 && !fetchedAlready) {
      setFetchedAlready(true);
      fetchData();
    }
  }, [
    location,
    loadItemGroups,
    itemGroups,
    itemGroupsPagination,
    fetchedAlready,
  ]);

  const onSortHandler = (column, order) => {
    const pagination = {
      sortBy: column,
      order,
      pageSize: itemGroupsPagination.pageSize,
      pageNumber: itemGroupsPagination.pageNumber,
    };
    history.push("/itemGroups?" + queryString.stringify(pagination));
    setFetchedAlready(false);
    clearItemGroups();
  };

  const onChangeRowsPerPageHandler = (rowsPerPage, pageNumber) => {
    let currentRow = itemGroupsPagination.pageSize * pageNumber;
    let totalPages = Math.ceil(itemGroupsPagination.totalCount / rowsPerPage);
    let newPageNumber = 1;
    for (let i = newPageNumber; i <= totalPages; i++) {
      if (i * rowsPerPage >= currentRow) {
        newPageNumber = i > 1 ? i - 1 : i;
        break;
      }
    }
    const pagination = {
      sortBy: itemGroupsPagination.sortBy,
      order: itemGroupsPagination.order,
      pageSize: rowsPerPage,
      pageNumber: newPageNumber,
    };
    history.push("/itemGroups?" + queryString.stringify(pagination));
    setFetchedAlready(false);
    clearItemGroups();
  };

  const onChangePageHandler = (pageNumber, totalPages) => {
    const pagination = {
      sortBy: itemGroupsPagination.sortBy,
      order: itemGroupsPagination.order,
      pageSize: itemGroupsPagination.pageSize,
      pageNumber: pageNumber > totalPages ? totalPages : pageNumber,
    };
    history.push("/itemGroups?" + queryString.stringify(pagination));
    setFetchedAlready(false);
    clearItemGroups();
  };

  const onSelectedRowChangeHandler = (selectedRows) => {
    setMarkedForDeletion(selectedRows);
  };

  const onDeleteClickedHandler = (event) => {
    event.preventDefault();
    const records = markedForDeletion.length;
    if (records <= 0) {
      return;
    }

    const message =
      "Are you sure you want to delete " +
      records +
      " item" +
      (records > 1 ? "s?" : "?");
    confirmAlert({
      title: "Deletions cannot be undone.",
      message: message,
      buttons: [
        {
          label: "Yes",
          onClick: () => {
            event.preventDefault();
            markedForDeletion.forEach((row) => deleteItemGroup(row.id));
            setFetchedAlready(false);
            clearItemGroups();
          },
        },
        {
          label: "No",
          onClick: () => {
            event.preventDefault();
          },
        },
      ],
    });
  };

  const onAddClickedHandler = (event) => {
    event.preventDefault();
    history.push("/itemGroups/add");
  };

  const onSearchHandler = (value) => {
    const pagination = {
      name: value,
      sortBy: itemGroupsPagination.sortBy,
      order: itemGroupsPagination.order,
      pageSize: itemGroupsPagination.pageSize,
      pageNumber:
        itemGroupsPagination.pageNumber <= 0
          ? 1
          : itemGroupsPagination.pageNumber,
    };
    history.push("/itemGroups?" + queryString.stringify(pagination));
    setFetchedAlready(false);
    clearItemGroups();
  };

  return (
    <>
      <h2>ItemGroups</h2>
      <p>Returns a list of max prices of items grouped by item name</p>
      <ItemGroupList
        itemGroups={itemGroups}
        location={location}
        history={history}
        onSort={onSortHandler}
        onChangeRowsPerPage={onChangeRowsPerPageHandler}
        onChangePage={onChangePageHandler}
        onSelectedRowsChange={onSelectedRowChangeHandler}
        onDeleteClicked={onDeleteClickedHandler}
        onAddClicked={onAddClickedHandler}
        onSearch={onSearchHandler}
        itemGroupsPagination={itemGroupsPagination}
      />
    </>
  );
}

ItemGroupsPage.propTypes = {
  itemGroups: PropTypes.array.isRequired,
  itemGroupsPagination: PropTypes.object,
  // isLoadingItemGroups: PropTypes.bool.isRequired,
  loadItemGroups: PropTypes.func.isRequired,
  location: PropTypes.object.isRequired,
  history: PropTypes.object.isRequired,
};

function mapStateToProps(state, ownProps) {
  const values = queryString.parse(ownProps.location.search);
  const queryParameters = {
    ...state.itemGroupsPagination,
    sortBy: values.sortBy,
    order: values.order,
    pageSize: values.pageSize ? parseInt(values.pageSize) : 25,
    pageNumber: values.pageNumber ? parseInt(values.pageNumber) : 1,
  };

  return {
    itemGroups: state.itemGroups,
    itemGroupsPagination: queryParameters,
  };
}

const mapDispatchToProps = {
  loadItemGroups: itemGroupActions.loadItemGroups,
  clearItemGroups: itemGroupActions.clearItemGroups,
  deleteItemGroup: itemGroupActions.deleteItemGroup,
};

export default connect(mapStateToProps, mapDispatchToProps)(ItemGroupsPage);
