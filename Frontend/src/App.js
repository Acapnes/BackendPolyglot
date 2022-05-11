import React, { Component } from "react";
import CategoryList from "./CategoryList";
import Navi from "./Navi";
import ProductList from "./ProductList";
import { Container, Row, Col } from "reactstrap";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import NotFound from "./NotFound";

export default class App extends Component {
  state = { currentCategory: "", products: [], cart: [] };

  componentDidMount() {
    this.getProducts();
  }

  changeCategory = (category) => {
    this.setState({ currentCategory: category.categoryName });
    this.getProducts(category.id);
  };

  getProducts = (categoryId) => {
    let url = "http://localhost:3000/products";
    if (categoryId) {
      url += "?categoryId=" + categoryId;
    }
    fetch(url)
      .then((resp) => resp.json())
      .then((data) => this.setState({ products: data }));
  };

  addtoCart = (product) => {
    let newCart = this.state.cart;
    var addedItem = newCart.find((c) => c.product.id === product.id);
    if (addedItem) {
      addedItem.quantity += 1;
    } else {
      newCart.push({ product: product, quantity: 1 });
    }
    this.setState({ cart: newCart });
    console.log(this.state.cart);
  };

  render() {
    let productInfo = { title: "Product Info", options: "null" };
    let categoryInfo = { title: "Category Info", options: "null" };
    return (
      <Router>
        <div>
          <Container>
            <Navi cart={this.state.cart} />

            <Row>
              <Col xs="3">
                <CategoryList
                  currentCategory={this.state.currentCategory}
                  changeCategory={this.changeCategory}
                  info={categoryInfo}
                />
              </Col>
              <Col xs="9">
                {/* örnek bir title değişkeni (herhangi bir şey olabilir) alt Componentlere aktarılıyor */}
                {/* <Routes>
                <Route exact path="/" component={NotFound} />
              </Routes> */}
              </Col>
            </Row>
          </Container>
        </div>
      </Router>
    );
  }
}
