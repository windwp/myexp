var parser=require('../calc.js');
describe("A suite", function() {
  it("test - minus operator", function() {
    expect(parser.parse('90+-15')).toBe(75);
    expect(parser.parse('-90+15')).toBe(-75);
    expect(parser.parse('90-15')).toBe(75);
    
  });
  it("test - frac operator", function() {
    expect(parser.parse('\\frac{20}{5}')).toBe(4);
    
  });
});