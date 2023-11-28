using UnityEngine;

public class PanAttack : MonoBehaviour {
    private Animation anim;
    private HitBox panHitBox;
    public bool canDamage { get; private set; } = false;

    private void Start() {
        anim = GetComponent<Animation>();
        panHitBox = GetComponent<HitBox>();
    }

    public void ExecutePanSlam() {
        anim.Play("PanSlam");
        panHitBox.canDamage = true;
    }
}
