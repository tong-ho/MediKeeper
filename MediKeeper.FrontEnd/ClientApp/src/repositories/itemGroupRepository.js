import Axios from "axios";
import PropTypes from "prop-types";

const baseUrl = process.env.REACT_APP_API_URL + "/itemGroups";

export async function getItemGroups(queryParameters) {
  try {
    var response = await Axios.get(baseUrl, {
      params: {
        ...queryParameters,
      },
    });

    return {
      itemGroupsPagination: JSON.parse(response.headers["paginationmetadata"]),
      itemGroups: response.data,
    };
  } catch (error) {
    console.error("API call failed: " + error);
    throw error;
  }
}

getItemGroups.propTypes = {
  queryParameters: PropTypes.shape({
    sortBy: PropTypes.string,
    order: PropTypes.string,
    pageSize: PropTypes.number,
    pageNumber: PropTypes.number,
  }).isRequired,
};

export async function get(id) {
  try {
    const response = await Axios.get(baseUrl + "/" + id);
    return response.data;
  } catch (error) {
    throw error;
  }
}

export async function remove(id) {
  try {
      const response = await Axios.delete(baseUrl + "/" + id);
    return response;
  } catch (error) {
    throw error;
  }
}

export async function save(itemGroup) {
  try {
    const axiosConfig = {
      headers: {
        "Content-Type": "application/json;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
      },
    };

    if (!itemGroup.id || itemGroup.id === 0) {
      const response = await Axios.post(baseUrl, itemGroup);
      return response.data;
    } else {
      const response = await Axios.put(
          baseUrl + "/" + itemGroup.id,
        itemGroup,
        axiosConfig
      );
      return itemGroup;
    }
  } catch (error) {
    throw error;
  }
}
