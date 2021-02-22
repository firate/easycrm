import { Container, Row, Col } from 'reactstrap';

import Sidebar from './sidebar/Sidebar';

function App() {
  return (
    <Container>
      <Row>
      <Col>
        <Sidebar />
      </Col>
      </Row>
    </Container>
  );
}

export default App;
