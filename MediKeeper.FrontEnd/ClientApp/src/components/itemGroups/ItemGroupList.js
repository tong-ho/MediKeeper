import React from "react";
import PropTypes from "prop-types";
import DataTable from "react-data-table-component";
import { faTrash, faPlus } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { DebounceInput } from "react-debounce-input";

function ItemGroupList({
  itemGroups,
  onSort,
  onChangeRowsPerPage,
  onChangePage,
  onSelectedRowsChange,
  onDeleteClicked,
  onAddClicked,
  onSearch,
  itemGroupsPagination,
}) {
  const columns = [
    {
      name: "Name",
      selector: "name",
      sortable: true,
      cell: (itemGroup) => (
        <a href={"/itemGroups/" + itemGroup.id}>{itemGroup.name}</a>
      ),
    },
    {
      name: "Cost",
      selector: "cost",
      sortable: true,
    },
  ];

  return (
    <DataTable
      columns={columns}
      data={itemGroups}
      defaultSortField="name"
      highlightOnHover={true}
      striped={true}
      progressPending={!itemGroups}
      noHeader={true}
      sortServer={true}
      clearSelectedRows={true}
      onSort={(column, order) =>
        onSort(column.selector, order === "desc" ? "descending" : "ascending")
      }
      pagination={true}
      paginationPerPage={itemGroupsPagination.pageSize ?? 10}
      paginationServer={true}
      paginationRowsPerPageOptions={[10, 25, 50, 100]}
      paginationTotalRows={itemGroupsPagination.totalCount}
      onChangePage={onChangePage}
      onChangeRowsPerPage={onChangeRowsPerPage}
      selectableRows={true}
      onSelectedRowsChange={({ selectedRows }) =>
        onSelectedRowsChange(selectedRows)
      }
      subHeader={true}
      subHeaderComponent={
        <div style={{ display: "flex", alignItems: "center" }}>
          <DebounceInput
            style={{ margin: "5px", height: 40 }}
            debounceTimeout={300}
            placeholder="Search"
            onChange={(event) => onSearch(event.target.value)}
          />
          <button
            type="button"
            className="btn btn-danger ml-1"
            onClick={onDeleteClicked}
          >
            <FontAwesomeIcon icon={faTrash} style={{ width: 25, height: 25 }} />
          </button>
          <button
            type="button"
            className="btn btn-primary ml-1"
            onClick={onAddClicked}
          >
            <FontAwesomeIcon icon={faPlus} style={{ width: 25, height: 25 }} />
          </button>
        </div>
      }
      subHeaderAlign="right"
    />
  );
}

ItemGroupList.propTypes = {
  itemGroups: PropTypes.array.isRequired,
  onSort: PropTypes.func.isRequired,
  onChangeRowsPerPage: PropTypes.func.isRequired,
  onChangePage: PropTypes.func.isRequired,
  onSelectedRowsChange: PropTypes.func.isRequired,
  onDeleteClicked: PropTypes.func.isRequired,
  onAddClicked: PropTypes.func.isRequired,
  onSearch: PropTypes.func.isRequired,
  itemGroupsPagination: PropTypes.object.isRequired,
};

export default ItemGroupList;
