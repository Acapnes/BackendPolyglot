import React, { Component } from 'react'
import {ListGroup,ListGroupItem} from "reactstrap";

export default class ProductList extends Component {
  render() {
    return (
      <div>
      <h3>{this.props.info.title}</h3>
    </div>
    )
  }
}
