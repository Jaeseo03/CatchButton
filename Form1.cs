namespace CatchButton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnRunAway_MouseEnter(object sender, EventArgs e)
        {
            // 1. 난수 생성기 준비
            Random rd= new Random();

            // 2. 가용영역계산(버튼이 폼테두리에 걸리지 않게 보호)
            // ⚠️ 중요: Location은 버튼의 왼쪽 위 모서리 좌표이므로, 버튼의 크기를 빼야 전체가 폼 안에 들어옴
            // ClientSize: 타이틀바와테두리를제외한실제흰도화지영역
            int maxX= this.ClientSize.Width - btnRunAway.Width;      // 버튼 너비만큼 감소
            int maxY= this.ClientSize.Height - btnRunAway.Height;    // 버튼 높이만큼 감소

            // 3. 음수 좌표 방지(maxX, maxY가 음수가 되는 경우 대비)
            // 폼 크기가 버튼보다 작으면 최소값을 0으로 설정
            maxX= Math.Max(0, maxX);
            maxY= Math.Max(0, maxY);

            // 4. 랜덤 좌표 추출
            // maxX가 0이면 0~0 범위에서 무조건 0 반환
            // maxX가 양수면 0~maxX 범위에서 랜덤값 반환
            int nextX= rd.Next(0, maxX + 1);    // +1: Next의 상한은 exclusive이므로 포함시키기 위해
            int nextY= rd.Next(0, maxY + 1);    // +1: Next의 상한은 exclusive이므로 포함시키기 위해

            // 5. 위치 할당(새로운Point 객체생성)
            btnRunAway.Location= new Point(nextX, nextY);

            // 6. 시각적 피드백(폼 제목 표시줄에 좌표출력)
            this.Text= $"버튼위치: ({nextX}, {nextY})";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
