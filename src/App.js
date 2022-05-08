import CategoryList from "./CategoryList";
import Navi from "./Navi";
import ProductList from "./ProductList";
import { Container, Row, Col } from "reactstrap";

function App() {
  let productInfo = { title: "Product Info", options: "null" };
  let categoryInfo = { title: "Category Info", options: "null" };
  return (
    <div>
      <Container>
        <Row>
          <Navi />
        </Row>
        <Row>
          <Col xs="3">
            <CategoryList info={categoryInfo} />
          </Col>
          <Col xs="9">
            {/* örnek bir title değişkeni (herhangi bir şey olabilir) alt Componentlere aktarılıyor */}
            <ProductList info={productInfo} />
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default App;
