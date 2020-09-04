import React from "react";
import PropTypes from "prop-types";

function ItemGroupDetails({ itemGroup }) {
  return (
    <table className="table">
      <tbody>
        <tr>
          <th scope="col">Cost:</th>
          <td>{itemGroup.cost}</td>
        </tr>
      </tbody>
    </table>
  );
}

ItemGroupDetails.propTypes = {
  itemGroup: PropTypes.object.isRequired,
};

export default ItemGroupDetails;
